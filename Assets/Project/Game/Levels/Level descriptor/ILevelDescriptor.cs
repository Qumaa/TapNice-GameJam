namespace Project.Game
{
    public interface ILevelDescriptor
    {
        int SceneIndex { get; }
        string SceneName { get; }
        string LevelName { get; }
    }
}