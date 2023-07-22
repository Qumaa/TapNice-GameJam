using System;

namespace Project.Game.Levels
{
    public interface ILevelUnlocker
    {
        event Action<int, string> OnLevelUnlocked;
        
        void UnlockLevel(int levelIndex);
        bool IsLevelUnlocked(int levelIndex);
        
        void UnlockLevel(string levelName);
        bool IsLevelUnlocked(string levelName);
    }

    public static class LevelUnlockerExtensions
    {
        public static bool TryUnlockLevel(this ILevelUnlocker unlocker, int levelIndex)
        {
            if (unlocker.IsLevelUnlocked(levelIndex))
                return false;
            
            unlocker.UnlockLevel(levelIndex);
            return true;
        }
        
        public static bool TryUnlockLevel(this ILevelUnlocker unlocker, string levelName)
        {
            if (unlocker.IsLevelUnlocked(levelName))
                return false;
            
            unlocker.UnlockLevel(levelName);
            return true;
        }
    }
}
