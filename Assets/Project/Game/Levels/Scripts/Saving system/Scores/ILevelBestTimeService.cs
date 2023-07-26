namespace Project.Game.Levels
{
    public interface ILevelBestTimeService : IPersistentDataSaver
    {
        LevelBestTime GetBestTime(int levelIndex);
        void SetBestTime(int levelIndex, LevelBestTime time);
    }
}