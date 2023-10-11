namespace SimHubApiPlugin.Models;

public record GameDataDto(
    string Speed,
    string BrakeBias,
    string Gear,
        
    float Rpm,
    float MaxRpm,
    float Redline,
    bool ShouldShift,
    float Turbo,
        
    string CurrentLapTime,
    string LastLapTime,
    string BestLapTime,
    string? FastestLapTime,
    float? DeltaInSeconds,
    string? DeltaFormatted,
        
    bool IsLapValid,
    int CurrentLap,
    int? TotalLaps,
    string SessionTimeLeft,
        
    bool IsInPitlane,
    bool IsPitLimiterOn,
        
    Assist? Tc1,
    Assist? Tc2,
    Assist? Abs,
        
    int? EngineMap,
    bool? IsLightOn,
        
    string? TyreCompound,
    WheelInfos WheelInfos,
        
    string FuelRemaining,
    string? FuelPerLap,
    string? FuelRemainingLaps,
    string? FuelTargetDelta,
    bool IsFuelAlertActive,
        
    FlagState FlagState,
        
    int Position,
    int TotalPositions,
        
    DrsState DrsState,
    ErsState? ErsState
);