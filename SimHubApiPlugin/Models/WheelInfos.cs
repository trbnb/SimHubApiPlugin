namespace SimHubApiPlugin.Models
{
    public record WheelInfos(
        WheelInfo FrontLeft,
        WheelInfo FrontRight,
        WheelInfo RearLeft,
        WheelInfo RearRight,

        string TemperatureUnit,
        string PressureUnit
    );
}