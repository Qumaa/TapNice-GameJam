using System;

namespace Project.Game.Levels
{
    public class LevelUnlocker : ILevelUnlocker
    {
        public event Action<int, string> OnLevelUnlocked;
        public void UnlockLevel(int levelIndex) =>
            throw new NotImplementedException();

        public bool IsLevelUnlocked(int levelIndex) =>
            throw new NotImplementedException();

        public void UnlockLevel(string levelName) =>
            throw new NotImplementedException();

        public bool IsLevelUnlocked(string levelName) =>
            throw new NotImplementedException();
    }
}