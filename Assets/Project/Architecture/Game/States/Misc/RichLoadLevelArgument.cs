using System;
using System.Runtime.InteropServices;

namespace Project.Architecture.States
{
    [StructLayout(LayoutKind.Auto)]
    public readonly struct RichLoadLevelArgument
    {
        public readonly int LevelIndex;
        public readonly Action LoadedCallback;

        public RichLoadLevelArgument(int levelIndex, Action loadedCallback)
        {
            LevelIndex = levelIndex;
            LoadedCallback = loadedCallback;
        }
    }
}