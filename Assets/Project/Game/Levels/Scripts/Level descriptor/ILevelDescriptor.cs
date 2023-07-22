namespace Project.Game.Levels
{
    public interface ILevelDescriptor
    {
        int SceneIndex { get; }
        string SceneName { get; }
        string LevelName { get; }
        bool UnlockedByDefault { get; }
    }
}