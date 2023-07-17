using System;

namespace Project.Architecture.States
{
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