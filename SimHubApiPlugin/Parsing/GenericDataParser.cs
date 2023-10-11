using System;
using GameReaderCommon;
using SimHubApiPlugin.Models;

namespace SimHubApiPlugin.Parsing;

public class GenericDataParser : IGameDataParser<GameData>
{
    public GameDataDto OnNewData(GameData gameData)
    {
        return new GameDataDto(
            Speed: gameData.Speed(),
            BrakeBias: gameData.BrakeBias(),
            Gear: gameData.Gear(),

            Rpm: gameData.Rpm(),
            MaxRpm: gameData.MaxRpm(),
            Redline: gameData.Redline(),
            ShouldShift: gameData.ShouldShift(),
            Turbo: gameData.Turbo(),

            CurrentLapTime: gameData.CurrentLapTime(),
            LastLapTime: gameData.LastLapTime(),
            BestLapTime: gameData.BestLapTime(),
            FastestLapTime: null,
            DeltaInSeconds: null,
            DeltaFormatted: null,
                
            IsLapValid: gameData.IsLapValid(),
            CurrentLap: gameData.CurrentLap(),
            TotalLaps: gameData.TotalLaps(),
            SessionTimeLeft: gameData.SessionTimeLeft(),

            IsInPitlane: gameData.IsInPitlane(),
            IsPitLimiterOn: gameData.IsPitLimiterOn(),

            Tc1: gameData.Tc1(),
            Tc2: null,
            Abs: gameData.Abs(),

            EngineMap: null,
            IsLightOn: null,
                
            TyreCompound: null,
            WheelInfos: gameData.WheelInfos(),
                
            FuelRemaining: gameData.FuelRemaining(),
            FuelPerLap: null,
            FuelRemainingLaps: null,
            FuelTargetDelta: null,
            IsFuelAlertActive: gameData.IsFuelAlertActive(),

            FlagState: gameData.FlagState(),

            Position: gameData.Position(),
            TotalPositions: gameData.TotalPositions(),

            DrsState: gameData.DrsState(),
            ErsState: null
        );
    }
}