namespace SimHubApiPlugin.Models
{
    public record PreparsedGameData(
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
        bool IsLapValid,
        int CurrentLap,
        int? TotalLaps,
        string SessionTimeLeft,
        bool IsInPitlane,
        bool IsPitLimiterOn,
        Assist? Tc1,
        Assist? Abs,
        WheelInfos WheelInfos,
        string FuelRemaining,
        bool IsFuelAlertActive,
        FlagState FlagState,
        int Position,
        int TotalPositions,
        DrsState DrsState
    );
}