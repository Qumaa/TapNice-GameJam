using System;
using UnityEngine.SceneManagement;

namespace Project.Architecture
{
    public class SyncSceneLoader : ISceneLoader
    {
        private readonly ISceneLoadingOperationHandler _handler;

        public SyncSceneLoader(ISceneLoadingOperationHandler handler)
        {
            _handler = handler;
        }

        public ISceneLoadingOperation LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            return new SyncSceneLoadingOperation(sceneName);
        }

        public ISceneLoadingOperation LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
            return new SyncSceneLoadingOperation(sceneIndex);
        }

        public ISceneLoadingOperationHandler GetHandler() =>
            _handler;
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