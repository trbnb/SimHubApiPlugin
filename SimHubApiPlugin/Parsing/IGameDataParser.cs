using SimHubApiPlugin.Models;

namespace SimHubApiPlugin.Parsing
{
    public interface IGameDataParser<in T> : IGameDataParser
        where T : GameReaderCommon.GameData
    {
        GameDataDto OnNewData(T gameData);
    }

    public interface IGameDataParser
    {
        GameDataDto OnNewData(GameReaderCommon.GameData gameData);
    }
}