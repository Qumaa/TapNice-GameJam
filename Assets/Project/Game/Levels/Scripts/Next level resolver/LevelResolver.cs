using System;

namespace Project.Game.Levels
{
    public class LevelResolver : ILevelResolver
    {
        private readonly int _maxLevels;

        public LevelResolver(int maxLevels)
        {
            _maxLevels = maxLevels;
        }

        public int CurrentLevel { get; private set; }

        public bool HasNextLevel(out int levelIndex)
        {
            var nextLevel = CurrentLevel + 1;
            var hasNextLevel = nextLevel < _maxLevels;
            
            levelIndex = hasNextLevel ? nextLevel : 0;
            return hasNextLevel;
        }

        public void SetLevel(int levelIndex)
        {
            if (levelIndex < 0 || levelIndex >= _maxLevels)
                throw new ArgumentException();
            
            CurrentLevel = levelIndex;
        }
    }
}