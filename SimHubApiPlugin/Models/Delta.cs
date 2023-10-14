using System;
using SimHubApiPlugin.Formatting;

namespace SimHubApiPlugin.Models;

public record Delta(
    string Formatted,
    Sign Sign
)
{
    public Delta(TimeSpan timeSpan) : this(
        Formatted: timeSpan.FormatDelta(),
        Sign: timeSpan.GetSign()
    ) {}
}

public static class DeltaExtensions
{
    public static Delta ToDelta(this TimeSpan timeSpan) => new(timeSpan);
}