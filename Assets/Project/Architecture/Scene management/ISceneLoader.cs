using System;

namespace Project.Architecture.SceneManagement
{
    public interface ISceneLoader
    {
        event Action OnNewSceneLoaded;
        ISceneLoadingOperation LoadScene(string sceneName);
        ISceneLoadingOperation LoadScene(int sceneIndex);
        ISceneLoadingOperationHandler GetHandler();
    }
    
    public static class SceneLoaderExtensions
    {
        // todo: this breaks when there the previous handling is not finished but this method is called. It's not a big concern but keep this in mind
        public static void LoadSceneHandled(this ISceneLoader loader, string sceneName, Action onCompleted) =>
            loader.GetHandler().HandleLoading(loader.LoadScene(sceneName), onCompleted);
        
        public static void LoadSceneHandled(this ISceneLoader loader, int sceneIndex, Action onCompleted) =>
            loader.GetHandler().HandleLoading(loader.LoadScene(sceneIndex), onCompleted);
    }
}