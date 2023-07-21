using System;
using System.Runtime.InteropServices;

namespace Project.Game.Levels
{
    [StructLayout(LayoutKind.Auto)]
    public readonly ref struct LevelBestTime
    {
        public readonly float AsSeconds;
        public readonly TimeSpan AsTimeSpan;

        public bool IsEmpty =>
            this.AsSeconds <= 0;

        public LevelBestTime(float seconds)
        {
            AsSeconds = seconds;
            AsTimeSpan = TimeSpan.FromSeconds(seconds);
        }

        public static LevelBestTime Empty() =>
            new();

        public static implicit operator LevelBestTime(float seconds) =>
            new(seconds);
    }
}