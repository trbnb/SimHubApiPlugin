using System;
using System.Linq;
using GameReaderCommon;
using PCarsSharedMemory.AMS2.Models;
using SimHubApiPlugin.Formatting;
using SimHubApiPlugin.Models;
using SimHubApiPlugin.Utils;

namespace SimHubApiPlugin.Parsing;

public class Automobilista2RawDataParser : RawDataParser<AMS2APIStruct>
{
    public Automobilista2RawDataParser(IGameDataParser fallback) : base(fallback) { }

    protected override GameDataDto OnNewData(
        GameData<AMS2APIStruct> gameData,
        AMS2APIStruct raw
    ) => Fallback.OnNewData(gameData) with
    {
        FastestLapTime = raw.mSessionFastestLapTime.ToTimeSpan().FormatLaptime(),
        DeltaInSeconds = raw.mSplitTime,
        DeltaFormatted = (raw.mSplitTime * 1000).ToTimeSpan().FormatDelta(),
        TyreCompound = raw.mTyreCompound.First().value ?? string.Empty,
        Tc1 = null
    };
}