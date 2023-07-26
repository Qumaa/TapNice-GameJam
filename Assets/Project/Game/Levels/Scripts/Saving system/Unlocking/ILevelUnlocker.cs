namespace Project.Game.Levels
{
    public interface ILevelUnlocker : IPersistentDataSaver
    {
        void UnlockLevel(int levelIndex);
        bool IsLevelUnlocked(int levelIndex);
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
    }
}
