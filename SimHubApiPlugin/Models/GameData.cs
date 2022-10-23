namespace SimHubApiPlugin
{
    public class GameData
    {
        public double Speed { get; set; }
        public double BrakeBias { get; set; }
        public string Gear { get; set; }

        public float Rpm { get; set; }
        public float MaxRpm { get; set; }
        public float Redline { get; set; }

        public long CurrentLapTime { get; set; }
        public long LastLapTime { get; set; }
        public long BestLapTime { get; set; }
        public float DeltaInSeconds { get; set; }
        public bool IsLapValid { get; set; }

        public int CurrentLap { get; set; }
        public int? TotalLaps { get; set; }
        public long SessionTimeLeft { get; set; }

        public bool IsInPitlane { get; set; }
        public bool IsPitLimiterOn { get; set; }

        public int TC1Level { get; set; }
        public bool TC1Active { get; set; }
        public int TC2 { get; set; }
        public bool TC2Active { get; set; }
        public int ABS { get; set; }
        public bool ABSActive { get; set; }

        public int? EngineMap { get; set; }
        public bool? IsLightOn { get; set; }
        
        public string TyreCompound { get; set; }
        public WheelInfos WheelInfos { get; set; }

        public float FuelRemaining { get; set; }
        public float FuelPerLap { get; set; }
        public float FuelRemainingLaps { get; set; }

        public FlagState FlagState { get; set; }

        public int Position { get; set; }
        public int TotalPositions { get; set; }

        public DrsState DrsState { get; set; }

        public bool ErsAvailable { get; set; }
        public float ErsBattery { get; set; }
        public double ErsAllowance { get; set; }
    }

    public class WheelInfo {
        public float Temperature { get; set; }
        public float Health { get; set; }
        public float Pressure { get; set; }
    }

    public class WheelInfos {
        public WheelInfo FrontLeft { get; set; }
        public WheelInfo FrontRight { get; set; }
        public WheelInfo RearLeft { get; set; }
        public WheelInfo RearRight { get; set; }
    }

    public enum DrsState {
        None, Available, Enabled
    }

    public class FlagState {
        public bool Black { get; set; }
        public bool Orange { get; set; }
        public bool Blue { get; set; }
        public bool Checkered { get; set; }
        public bool Green { get; set; }
        public string Name { get; set; }
        public bool White { get; set; }
        public bool Yellow { get; set; }
    }

    public class ErsState {
        public float BatteryCharge { get; set; }
        public float RemainingLapAllowance { get; set; }
        public int MguHMode { get; set; }
        public int MguKMode { get; set; }
        public int RecoveryLevel { get; set; }
        public int EngineBrake { get; set; }
    }
}