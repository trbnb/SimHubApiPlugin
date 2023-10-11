namespace SimHubApiPlugin.Models;

public record WheelInfo(
    float Temperature,
    float? Health,
    string? HealthFormatted,
    float Pressure
);