using GameReaderCommon;
using SimHub.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimHubApiPlugin
{
    [PluginDescription("Custom Api")]
    [PluginAuthor("Thorben")]
    [PluginName("SimHubApiPlugin")]
    public class SimHubApiPlugin : IPlugin, IDataPlugin
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public PluginManager PluginManager { get; set; }

        public void Init(PluginManager pluginManager)
        {
            SimHub.Logging.Current.Info("Starting custom API Plugin");
            Program.StartAsync(9999, cancellationTokenSource.Token);
        }

        public void End(PluginManager pluginManager)
        {
            cancellationTokenSource.Cancel();
        }

        public void DataUpdate(PluginManager pluginManager, ref GameReaderCommon.GameData data)
        {
            try
            {
                DataManager.Instance.OnNewData(ref data);
            }
            catch (Exception ex)
            {
                SimHub.Logging.Current.Error("Unable to parse data: " + ex.Message, ex);
            }
        }
    }
}
