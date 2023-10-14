namespace SimHubApiPlugin.Models;

public record WheelInfos(
    WheelInfo FrontLeft,
    WheelInfo FrontRight,
    WheelInfo RearLeft,
    WheelInfo RearRight
);

public record WheelInfo(
    float Temperature,
    TyreHealth? Health,
    string Pressure
);

public record TyreHealth(
    float Value,
    string Formatted
);