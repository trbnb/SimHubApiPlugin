namespace SimHubApiPlugin
{
    public class GameData
    {
        public float Speed { get; set; }
        public float BrakeBias { get; set; }
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

        public Assist TC1 { get; set; }
        public Assist TC2 { get; set; }
        public Assist ABS { get; set; }

        public int? EngineMap { get; set; }
        public bool? IsLightOn { get; set; }
        
        public string TyreCompound { get; set; }
        public WheelInfos WheelInfos { get; set; }

        public float FuelRemaining { get; set; }
        public float? FuelPerLap { get; set; }
        public float? FuelRemainingLaps { get; set; }

        public FlagState FlagState { get; set; }

        public int Position { get; set; }
        public int TotalPositions { get; set; }

        public DrsState DrsState { get; set; }

        public ErsState ErsState { get; set; }
    }
}