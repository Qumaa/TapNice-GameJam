namespace Project.Architecture
{
    public interface ISceneLoader
    {
        ISceneLoadingOperation LoadScene(string sceneName);
        ISceneLoadingOperation LoadScene(int sceneIndex);
    }
}