using SimHubApiPlugin.Models;

namespace SimHubApiPlugin
{
    public class ErsState {
        public float BatteryCharge { get; set; }
        public float RemainingLapAllowance { get; set; }
        public MguHMode MguHMode { get; set; }
        public MguKMode MguKMode { get; set; }
        public int RecoveryLevel { get; set; }
        public int EngineBrake { get; set; }
    }
}