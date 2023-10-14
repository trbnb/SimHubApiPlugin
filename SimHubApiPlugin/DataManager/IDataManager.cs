using System;
using GameReaderCommon;
using SimHub.Plugins;
using SimHubApiPlugin.Models;

namespace SimHubApiPlugin.DataManager;

public interface IDataManager
{
    public GameDataDto? GameDataDto { get; }

    public void OnNewData(GameData gameData);
}