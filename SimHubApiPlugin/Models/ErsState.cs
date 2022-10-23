namespace SimHubApiPlugin
{
    public class ErsState {
        public float BatteryCharge { get; set; }
        public float RemainingLapAllowance { get; set; }
        public int MguHMode { get; set; }
        public int MguKMode { get; set; }
        public int RecoveryLevel { get; set; }
        public int EngineBrake { get; set; }
    }
}