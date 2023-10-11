using SimHubApiPlugin.Models;

namespace SimHubApiPlugin.Models
{
    public record ErsState(
        float BatteryCharge,
        float RemainingLapAllowance,
        string MguHMode,
        string MguKMode,
        int RecoveryLevel,
        int EngineBrake
    );
}