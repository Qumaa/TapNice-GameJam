namespace Project.Game.Levels
{
    public interface ILevelBestTimeService : IPersistentDataProcessor
    {
        LevelBestTime GetBestTime(string levelName);
        void SetBestTime(string levelName, LevelBestTime time);
    }
}