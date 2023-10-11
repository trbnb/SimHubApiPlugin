using System;
using CodemastersReader.F12023.Packets;
using GameReaderCommon;
using SimHubApiPlugin.Models;

namespace SimHubApiPlugin.Parsing;

public class F123RawDataParser : RawDataParser<F12023TelemetryContainerEx>
{
    public F123RawDataParser(IGameDataParser fallback) : base(fallback) { }

    protected override GameDataDto OnNewData(
        GameData<F12023TelemetryContainerEx> gameData,
        F12023TelemetryContainerEx raw
    ) => Fallback.OnNewData(gameData) with
    {
        Tc1 = null,
        Abs = null,
        EngineMap = gameData.GameNewData.Raw.PlayerCarStatusData.m_fuelMix
    };
}