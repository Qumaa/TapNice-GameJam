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
}