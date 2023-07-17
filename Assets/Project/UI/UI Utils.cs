using System;

namespace Project.UI.Utils
{
    public static class UIUtils
    {
        private const string _SECONDS_FORMAT = @"s\.ff\s";
        private const string _MINUTES_FORMAT = @"m\:ss\m";
        private const string _HOURS_FORMAT = @"h\:mm\h";
        private const string _NAIL_FORMAL = "Y SO SLOW???";
        
        public static string FormatTime(float time)
        {
            var timeSpan = TimeSpan.FromSeconds(time);
            return timeSpan.ToString(SwitchFormat(timeSpan));
        }

        private static string SwitchFormat(TimeSpan time) =>
            time.Minutes == 0 ? _SECONDS_FORMAT :
            time.Hours == 0 ? _MINUTES_FORMAT :
            time.Days == 0 ? _HOURS_FORMAT :
            _NAIL_FORMAL;
    }
}