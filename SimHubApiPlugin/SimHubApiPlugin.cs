﻿using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using SimHub;
using SimHub.Plugins;
using SimHubApiPlugin.DataManager;
using SimHubApiPlugin.Web;

namespace SimHubApiPlugin;

[PluginDescription("Custom Api")]
[PluginAuthor("Thorben")]
[PluginName("CustomSimHubWsServerPlugin")]
// ReSharper disable once UnusedType.Global
public class SimHubApiPlugin : PluginBase, IDataPlugin
{
    private readonly CancellationTokenSource cancellationTokenSource = new();
    private IDataManager? currentDataManager;

    public override void Init(PluginManager pluginManager)
    {
        Logging.Current.Info("Starting custom API Plugin");
        IDataManager dataManager = new DataManager.DataManager(pluginManager);
        currentDataManager = dataManager;
        Startup.StartWebHost(
            port: 9999,
            cancellationToken: cancellationTokenSource.Token,
            configureServices: (_, collection) =>
            {
                collection.AddSingleton(dataManager);
            }
        );
    }

    public override void End(PluginManager pluginManager)
    {
        cancellationTokenSource.Cancel();
    }

    public void DataUpdate(PluginManager pluginManager, ref GameReaderCommon.GameData data)
    {
        try
        {
            currentDataManager?.OnNewData(data);
        }
        catch (Exception ex)
        {
            Logging.Current.Error("Unable to parse data: " + ex.Message, ex);
        }
    }
}