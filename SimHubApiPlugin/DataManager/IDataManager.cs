using System;
using GameReaderCommon;
using SimHub.Plugins;
using SimHubApiPlugin.Models;

namespace SimHubApiPlugin.DataManager;

public interface IDataManager : IDisposable
{
    public GameDataDto? GameDataDto { get; }

    public void Init(PluginManager pluginManager);
    public void OnNewData(GameData gameData);
}