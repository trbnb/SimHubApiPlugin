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
        Delta = raw.Graphics.iDeltaLapTime.ToTimeSpan().ToDelta(),
        EngineMap = raw.Graphics.EngineMap,
        LightsMode = raw.Graphics.LightsStage switch
        {
            0 => LightsMode.Off,
            1 => LightsMode.On,
            _ => LightsMode.HighBeam
        },
        IsRainlightOn = raw.Graphics.RainLights > 0,
        TyreCompound = raw.Graphics.TyreCompound.Let(it => it == "dry_compound" ? "DRY" : "WET"),
        FuelPerLap = raw.Graphics.FuelXLap.ToString("F2"),
        FuelRemainingLaps = raw.Graphics.fuelEstimatedLaps.ToString("F2")
    };
}