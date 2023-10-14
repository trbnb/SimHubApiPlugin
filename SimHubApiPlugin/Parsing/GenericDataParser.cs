using System;
using GameReaderCommon;
using SimHubApiPlugin.Models;
using SimHubApiPlugin.Utils;

namespace SimHubApiPlugin.Parsing;

public class GenericDataParser : IGameDataParser<GameData>
{
    public GameDataDto OnNewData(GameData gameData)
    {
        return new GameDataDto(
            Speed: gameData.Speed(),
            BrakeBias: gameData.BrakeBias(),
            Gear: gameData.Gear(),
            Throttle: gameData.NewData.Throttle.ToFloat(),
            Brake: gameData.NewData.Brake.ToFloat(),

            Rpm: gameData.Rpm(),
            MaxRpm: gameData.MaxRpm(),
            Redline: gameData.Redline(),
            ShouldShift: gameData.ShouldShift(),
            Turbo: gameData.Turbo(),
            Differential: null,

            CurrentLapTime: gameData.CurrentLapTime(),
            LastLapTime: gameData.LastLapTime(),
            BestLapTime: gameData.BestLapTime(),
            FastestLapTime: null,
            Delta: gameData.NewData.DeltaToSessionBest?.Let(delta => new TimeSpan((delta * 1000).ToLong()).ToDelta()),
            AllowedDelta: null,
                
            IsLapValid: gameData.IsLapValid(),
            CurrentLap: gameData.CurrentLap(),
            TotalLaps: gameData.TotalLaps(),
            SessionTimeLeft: gameData.SessionTimeLeft(),

            IsInPitlane: gameData.IsInPitlane(),
            IsPitLimiterOn: gameData.IsPitLimiterOn(),
            PitStopRejoinPrediction: null,
            PitStopWindow: null,

            Tc1: gameData.Tc1(),
            Tc2: null,
            Abs: gameData.Abs(),

            EngineMap: null,
            EngineMode: null,
            LightsMode: null,
            IsRainlightOn: null,
                
            TyreCompound: null,
            WheelInfos: gameData.WheelInfos(toTyreHealth: CommonParsing.ToTyreHealth),
                
            FuelRemaining: gameData.FuelRemaining(),
            FuelPerLap: null,
            FuelRemainingLaps: null,
            FuelTargetDelta: null,
            IsFuelAlertActive: gameData.IsFuelAlertActive(),

            FlagState: gameData.FlagState(),
            SafetyCarStatus: null,

            Position: gameData.Position(),
            TotalPositions: gameData.TotalPositions(),

            DrsState: gameData.DrsState(),
            ErsState: null
        );
    }
}