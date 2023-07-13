using System;

namespace Project.Architecture
{
    public interface ISceneLoader
    {
        event Action OnNewSceneLoaded;
        ISceneLoadingOperation LoadScene(string sceneName);
        ISceneLoadingOperation LoadScene(int sceneIndex);
        ISceneLoadingOperationHandler GetHandler();
    }
}