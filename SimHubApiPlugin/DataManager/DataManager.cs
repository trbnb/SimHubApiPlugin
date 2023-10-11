using ACSharedMemory;
using CodemastersReader;
using GameReaderCommon;
using PCarsSharedMemory.AMS2;
using SimHub.Plugins;
using SimHubApiPlugin.Models;
using SimHubApiPlugin.Parsing;
using SimHubApiPlugin.Utils;

namespace SimHubApiPlugin.DataManager;

public class DataManager : IDataManager
{
    public GameDataDto? GameDataDto { get; private set; }
        
    private IGameDataParser? parser;

    public void Init(PluginManager pluginManager)
    {
        var genericParser = new GenericDataParser();
        parser = pluginManager.GameManager switch
        {
            CodemastersF12023Manager _ => new F123RawDataParser(genericParser),
            ACManager _ => new AssettoCorsaRawDataParser(genericParser),
            ACCManager _ => new AssettoCorsaCompetizioneRawDataParser(genericParser),
            Automobilista2Manager _ => new Automobilista2RawDataParser(genericParser),
            _ => genericParser
        };
    }
        
    public void OnNewData(GameData gameData)
    {
        if (gameData.NewData == null) return;
            
        parser?.Let(it => GameDataDto = it.OnNewData(gameData));
    }

    public void Dispose()
    {
        parser = null;
    }
}