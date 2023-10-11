using System;
using ACSharedMemory.ACC.Reader;
using GameReaderCommon;
using SimHubApiPlugin.Formatting;
using SimHubApiPlugin.Models;
using SimHubApiPlugin.Utils;

namespace SimHubApiPlugin.Parsing;

public class AssettoCorsaCompetizioneRawDataParser : RawDataParser<ACCRawData>
{
    public AssettoCorsaCompetizioneRawDataParser(IGameDataParser fallback) : base(fallback) { }
        
    protected override GameDataDto OnNewData(
        GameData<ACCRawData> gameData,
        ACCRawData raw
    ) => Fallback.OnNewData(gameData) with
    {
        Tc2 = new Assist(
            Level: raw.Graphics.TCCut,
            IsActive: false
        ),
        FastestLapTime = raw.Realtime.BestSessionLap.LaptimeMS?.ToTimeSpan().FormatLaptime(),
        DeltaInSeconds = raw.Graphics.iDeltaLapTime / 1000f,
        DeltaFormatted = raw.Graphics.iDeltaLapTime.ToTimeSpan().FormatDelta(),
        EngineMap = raw.Graphics.EngineMap,
        IsLightOn = raw.Graphics.LightsStage > 0,
        TyreCompound = raw.Graphics.TyreCompound.Let(it => it == "dry_compound" ? "DRY" : "WET"),
        FuelPerLap = raw.Graphics.FuelXLap.ToString("F2"),
        FuelRemainingLaps = raw.Graphics.fuelEstimatedLaps.ToString("F2")
    };
}