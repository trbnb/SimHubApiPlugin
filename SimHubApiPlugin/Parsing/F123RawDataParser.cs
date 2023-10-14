using System;
using System.Linq;
using CodemastersReader;
using CodemastersReader.F12023.Packets;
using GameReaderCommon;
using SimHubApiPlugin.Models;
using SimHubApiPlugin.Utils;

namespace SimHubApiPlugin.Parsing;

public class F123RawDataParser : RawDataParser<F12023TelemetryContainerEx>
{
    public F123RawDataParser(IGameDataParser fallback) : base(fallback) { }

    protected override GameDataDto OnNewData(
        GameData<F12023TelemetryContainerEx> gameData,
        F12023TelemetryContainerEx raw
    ) => Fallback.OnNewData(gameData) with
    {
        Differential = raw.PlayerCarSetup.m_onThrottle.ToString("F0"),
        Tc1 = null,
        Abs = null,
        EngineMode = raw.PlayerCarStatusData.m_fuelMix
            .Let(fuelMixIndex => $"({fuelMixIndex + 1}) " + fuelMixIndex switch
            {
                0 => "Lean",
                1 => "Standard",
                2 => "Rich",
                3 => "MAX",
                _ => "?"
            }),
        AllowedDelta = (raw.PlayerLapData.m_safetyCarDelta * 1000).ToTimeSpan().ToDelta(),
        PitStopRejoinPrediction = raw.PacketSessionData.m_pitStopRejoinPosition + 1 + ".",
        PitStopWindow = $"L{raw.PacketSessionData.m_pitStopWindowIdealLap}-L{raw.PacketSessionData.m_pitStopWindowLatestLap}",
        // 0 = no safety car, 1 = full, 2 = virtual, 3 = formation lap
        SafetyCarStatus = raw.PacketSessionData.m_safetyCarStatus
            .Let(status => status switch
            {
                1 => SafetyCarStatus.SafetyCar,
                2 => SafetyCarStatus.VirtualSafetyCar,
                _ => SafetyCarStatus.None
            }),
        ErsState = new ErsState(
            BatteryCharge: raw.PlayerCarStatusData.m_ersStoreEnergy / gameData.GameNewData.ERSMax.ToFloat(),
            RemainingLapAllowance: 1 - (raw.PlayerCarStatusData.m_ersDeployedThisLap /  gameData.GameNewData.ERSMax).ToFloat(),
            MguHMode: null,
            MguKMode: raw.PlayerCarStatusData.m_ersDeployMode
                .Let(mode => $"({mode}) " + mode switch
                {
                    0 => "None",
                    1 => "Medium",
                    2 => "Hotlap",
                    3 => "Overtake",
                    _ => "?"
                }),
            RecoveryLevel: null,
            EngineBrake: null
        ),
        FuelTargetDelta = raw.PlayerCarStatusData.m_fuelRemainingLaps.ToString("F2") + " Laps",
        FuelRemaining = raw.PlayerCarStatusData.m_fuelInTank.ToString("F2") + " L",
    };
}