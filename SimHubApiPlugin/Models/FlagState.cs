namespace SimHubApiPlugin.Models
{
    public record FlagState(
        bool Black,
        bool Orange,
        bool Blue,
        bool Checkered,
        bool Green,
        string Name,
        bool White,
        bool Yellow
    );
}