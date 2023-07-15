using System;
using UnityEngine.SceneManagement;

namespace Project.Architecture.SceneManagement
{
    public class SyncSceneLoader : ISceneLoader
    {
        private readonly ISceneLoadingOperationHandler _handler;
        private ISceneLoadingOperation _currentOperation;

        public event Action OnNewSceneLoaded;

        public SyncSceneLoader(ISceneLoadingOperationHandler handler)
        {
            _handler = handler;
        }

        public ISceneLoadingOperation LoadScene(string sceneName) =>
            LoadScene(SceneManager.GetSceneByName(sceneName).buildIndex);

        public ISceneLoadingOperation LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
            _currentOperation = new SyncSceneLoadingOperation(sceneIndex);

            _currentOperation.OnLoadingCompleted += HandleLoadingCompleted;
            
            return _currentOperation;
        }

        private void HandleLoadingCompleted()
        {
            _currentOperation.OnLoadingCompleted -= HandleLoadingCompleted;
            OnNewSceneLoaded?.Invoke();
            _currentOperation = null;
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