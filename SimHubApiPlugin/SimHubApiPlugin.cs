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
        public PluginManager PluginManager { get; set; }

        public IDisposable disposable = null;

        public void Init(PluginManager pluginManager)
        {
            SimHub.Logging.Current.Info("Starting custom API Plugin");
            try
            {
                disposable = Program.Start(9999);
            }
            catch (Exception ex)
            {
                SimHub.Logging.Current.Error("Unable to start API", ex);
            }
        }

        public void End(PluginManager pluginManager)
        {
            disposable.Dispose();
        }

        public void DataUpdate(PluginManager pluginManager, ref GameReaderCommon.GameData data)
        {
            DataManager.Instance.OnNewData(ref data);
        }
    }
}
