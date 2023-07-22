using System;

namespace Project.Game.Levels
{
    public class NextLevelResolver : INextLevelResolver
    {
        private readonly ILevelUnlocker _levelUnlocker;
        private readonly int _maxLevels;
        private int _currentLevel;

        public NextLevelResolver(int maxLevels, ILevelUnlocker levelUnlocker)
        {
            _maxLevels = maxLevels;
            _levelUnlocker = levelUnlocker;
        }

        public bool HasNextLevel(out int levelIndex)
        {
            var nextLevel = _currentLevel + 1;
            var hasNextLevel = nextLevel < _maxLevels;
            
            levelIndex = hasNextLevel ? nextLevel : 0;
            return hasNextLevel;
        }

        public void SetLevel(int levelIndex)
        {
            if (levelIndex < 0 || levelIndex >= _maxLevels)
                throw new ArgumentException();
            
            _currentLevel = levelIndex;
        }
    }
}