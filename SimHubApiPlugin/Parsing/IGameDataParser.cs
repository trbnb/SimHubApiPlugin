using GameReaderCommon;
using SimHubApiPlugin.Models;

namespace SimHubApiPlugin.Parsing;

public interface IGameDataParser<in T> : IGameDataParser
    where T : GameData
{
    GameDataDto OnNewData(T gameData);
}

public interface IGameDataParser
{
    GameDataDto OnNewData(GameData gameData);
}