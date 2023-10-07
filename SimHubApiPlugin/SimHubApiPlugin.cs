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
    [PluginName("CustomSimHubWsServerPlugin")]
    public class SimHubApiPlugin : PluginBase, IDataPlugin
    {
        public override void Init(PluginManager pluginManager)
        {
            SimHub.Logging.Current.Info("Starting custom API Plugin");
            try
            {
                Startup.Start(9999);
            }
            catch (Exception e)
            {
                SimHub.Logging.Current.Error("Failed", e);
                throw;
            }
            
            SimHub.Logging.Current.Info("Success!");
        }

        public override void End(PluginManager pluginManager)
        {
            
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
