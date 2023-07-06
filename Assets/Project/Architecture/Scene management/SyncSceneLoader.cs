using UnityEngine.SceneManagement;

namespace Project.Architecture
{
    public class SyncSceneLoader : ISceneLoader
    {
        public ISceneLoadingOperation LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            return null;
        }

        public ISceneLoadingOperation LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
            return null;
        }
    }
}