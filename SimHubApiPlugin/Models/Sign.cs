using System;

namespace SimHubApiPlugin.Models;

public enum Sign
{
    Positive,
    Negative,
    Neutral
}

public static class SignExtensions
{
    public static string Format(this Sign sign) => sign switch
    {
        Sign.Positive => "+",
        Sign.Negative => "-",
        Sign.Neutral => "±",
        _ => ""
    };

    public static Sign GetSign(this TimeSpan timeSpan) => timeSpan.Milliseconds switch
    {
        > 0 => Sign.Positive,
        < 0 => Sign.Negative,
        _ => Sign.Neutral
    };
}