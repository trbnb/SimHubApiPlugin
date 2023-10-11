using System;

namespace SimHubApiPlugin.Formatting
{
    public static class CommonFormats
    {
        private const string LapTime = @"m\:ss\.fff";
        private const string SessionTimeLong = @"h\:mm\:ss";
        private const string SessionTimeShort = @"m\:ss";
        private const string Delta = @"s\.fff";

        public static string FormatLaptime(this TimeSpan timeSpan) => timeSpan.ToString(LapTime);
        public static string FormatSessionTime(this TimeSpan timeSpan) => timeSpan
            .ToString(timeSpan.TotalHours >= 1 ? SessionTimeLong : SessionTimeShort);

        public static string FormatDelta(this TimeSpan timeSpan) => timeSpan.TotalMilliseconds switch
        {
            < 0 => "-",
            > 0 => "+",
            _ => "±"
        } + timeSpan.ToString(Delta);
    }
}