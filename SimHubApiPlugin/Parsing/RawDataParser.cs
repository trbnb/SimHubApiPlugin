using GameReaderCommon;
using SimHubApiPlugin.Models;

namespace SimHubApiPlugin.Parsing
{
    public abstract class RawDataParser<T> : IGameDataParser<GameData<T>>
    {
        protected IGameDataParser Fallback { get; }

        protected RawDataParser(IGameDataParser fallback)
        {
            Fallback = fallback;
        }

        public GameDataDto OnNewData(GameData<T> gameData) => OnNewData(gameData, gameData.GameNewData.Raw);

        protected abstract GameDataDto OnNewData(GameData<T> gameData, T raw);

        public GameDataDto OnNewData(GameReaderCommon.GameData gameData)
        {
            return gameData switch
            {
                GameData<T> typedGameData => OnNewData(typedGameData),
                _ => Fallback.OnNewData(gameData)
            };
        }
    }
}