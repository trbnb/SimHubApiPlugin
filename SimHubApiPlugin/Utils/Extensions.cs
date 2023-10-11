using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimHubApiPlugin.Utils
{
    internal static class Extensions
    {
        internal static float ToFloat(this double number) => Convert.ToSingle(number);
        internal static long ToLong(this double number) => Convert.ToInt64(number);

        internal static TimeSpan ToTimeSpan(this int number) => new(number);
        internal static TimeSpan ToTimeSpan(this float number) => new((int)number);

        internal static T? TakeUnless<T>(this T receiver, Func<T, bool> transform)
            where T : class
        {
            return transform(receiver) ? null : receiver;
        }

        internal static T? TakeUnlessStruct<T>(this T receiver, Func<T, bool> transform)
            where T : struct
        {
            return transform(receiver) ? null : receiver;
        }

        internal static TOut Let<TIn, TOut>(this TIn receiver, Func<TIn, TOut> transform) => transform(receiver);

        internal static T Also<T>(this T receiver, Action<T> action)
        {
            action(receiver);
            return receiver;
        }
    }
}
