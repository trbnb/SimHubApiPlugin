// ReSharper disable NotAccessedPositionalProperty.Global
namespace SimHubApiPlugin.Models;

public record GameDataDto(
    string Speed,
    string BrakeBias,
    string Gear,
    float Throttle,
    float Brake,

    float Rpm,
    float MaxRpm,
    float Redline,
    bool ShouldShift,
    float Turbo,
    string? Differential,

    string CurrentLapTime,
    string LastLapTime,
    string BestLapTime,
    string? FastestLapTime,
    Delta? Delta,
    Delta? AllowedDelta,

    bool IsLapValid,
    int CurrentLap,
    int? TotalLaps,
    string SessionTimeLeft,

    bool IsInPitlane,
    bool IsPitLimiterOn,
    string? PitStopRejoinPrediction,
    string? PitStopWindow,

    Assist? Tc1,
    Assist? Tc2,
    Assist? Abs,

    int? EngineMap,
    string? EngineMode,
    LightsMode? LightsMode,
    bool? IsRainlightOn,

    string? TyreCompound,
    WheelInfos WheelInfos,

    string FuelRemaining,
    string? FuelPerLap,
    string? FuelRemainingLaps,
    string? FuelTargetDelta,
    bool IsFuelAlertActive,

    FlagState FlagState,
    SafetyCarStatus? SafetyCarStatus,

    int Position,
    int TotalPositions,

    DrsState DrsState,
    ErsState? ErsState
);