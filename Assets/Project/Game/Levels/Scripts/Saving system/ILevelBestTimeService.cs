namespace Project.Game.Levels
{
    public interface ILevelBestTimeService
    {
        LevelBestTime GetBestTime(string levelName);
        void SetBestTime(string levelName, LevelBestTime time);
        void LoadSavedData();
        void SaveLoadedData();
    }
}