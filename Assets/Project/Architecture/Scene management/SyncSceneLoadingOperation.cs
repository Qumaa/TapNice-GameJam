using System;
using UnityEngine.SceneManagement;

namespace Project.Architecture
{
    public class SyncSceneLoadingOperation : ISceneLoadingOperation
    {
        private readonly Scene _targetScene;
        
        public event Action OnLoadingCompleted;
        public bool IsDone { get; private set; }

        private SyncSceneLoadingOperation(Scene targetScene)
        {
            _targetScene = targetScene;
            SceneManager.sceneLoaded += NotifyDone;
        }

        public SyncSceneLoadingOperation(string sceneName) : this(SceneManager.GetSceneByName(sceneName))
        {
        }
        public SyncSceneLoadingOperation(int sceneIndex) : this(SceneManager.GetSceneByBuildIndex(sceneIndex))
        {
        }

        private void NotifyDone(Scene loadedScene, LoadSceneMode loadMode)
        {
            if (loadedScene != _targetScene)
                return;

            SceneManager.sceneLoaded -= NotifyDone;
            IsDone = true;
            OnLoadingCompleted?.Invoke();
        }
    }
}