namespace SimHubApiPlugin.Models
{
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
    }
}