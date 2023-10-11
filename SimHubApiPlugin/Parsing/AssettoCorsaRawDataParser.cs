using System;
using ACSharedMemory.Reader;
using GameReaderCommon;
using SimHubApiPlugin.Formatting;
using SimHubApiPlugin.Models;
using SimHubApiPlugin.Utils;

namespace SimHubApiPlugin.Parsing;

public class AssettoCorsaRawDataParser : RawDataParser<ACRawData>
{
    public AssettoCorsaRawDataParser(IGameDataParser fallback) : base(fallback) { }

    protected override GameDataDto OnNewData(
        GameData<ACRawData> gameData,
        ACRawData raw
    )
    {
        var fallbackData = Fallback.OnNewData(gameData);
        return fallbackData with
        {
            DeltaInSeconds = raw.Physics.PerformanceMeter,
            DeltaFormatted = raw.Physics.PerformanceMeter
                .Let(it => new TimeSpan((int)it * 1000))
                .FormatDelta(),
            TyreCompound = raw.Graphics.TyreCompound,
            Tc1 = fallbackData.Tc1?.TakeUnless(_ => raw.Extensions.TCPresetCount > 1),
            Abs = fallbackData.Abs?.TakeUnless(_ => raw.Extensions.ABSPresetCount > 1),
            ErsState = raw.TakeUnless(it => it.StaticInfo.HasERS == 0)?.Let(_ => new ErsState(
                RemainingLapAllowance: (1 - (gameData.NewData.ERSPercent / 100)).ToFloat(),
                BatteryCharge: raw.Physics.KersCharge,
                MguHMode: ((MguHMode)raw.Physics.ErsHeatCharging).ToString(),
                MguKMode: ((MguKMode)raw.Physics.ErsPowerLevel).ToString(),
                RecoveryLevel: raw.Physics.ErsRecoveryLevel,
                EngineBrake: raw.Physics.EngineBrake + 1
            ))
        };
    }
}