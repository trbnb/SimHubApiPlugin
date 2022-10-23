namespace SimHubApiPlugin
{
    public class WheelInfos {
        public WheelInfo FrontLeft { get; set; }
        public WheelInfo FrontRight { get; set; }
        public WheelInfo RearLeft { get; set; }
        public WheelInfo RearRight { get; set; }

        public string TemperatureUnit { get; set; }
        public string PressureUnit { get; set; }
    }
}