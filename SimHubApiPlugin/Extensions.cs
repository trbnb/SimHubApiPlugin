using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimHubApiPlugin
{
    internal static class Extensions
    {
        internal static float ToFloat(this double number) => Convert.ToSingle(number);
        internal static long ToLong(this double number) => Convert.ToInt64(number);

        internal static T? TakeUnless<T>(this T receiver, Func<T, bool> transform)
            where T : struct
        {
            if (transform(receiver))
            {
                return receiver;
            }
            else
            {
                return null;
            }
        }

        internal static TOut Let<TIn, TOut>(this TIn receiver, Func<TIn, TOut> transform) => transform(receiver);

        internal static DrsState ToDrsState(this GameReaderCommon.GameData data)
        {
            if (data.NewData.IsInPitLane == 1)
            {
                return DrsState.None;
            }
            else if (data.NewData.DRSEnabled == 1)
            {
                return DrsState.Enabled;
            }
            else if (data.NewData.DRSAvailable == 1)
            {
                return DrsState.Available;
            }
            else
            {
                return DrsState.None;
            }
        }
    }
}
