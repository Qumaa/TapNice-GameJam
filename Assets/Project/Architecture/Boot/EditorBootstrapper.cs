using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Architecture.Boot
{
    public class EditorBootstrapper : MonoBehaviour
    {
        private void Start()
        {
            #if !UNITY_EDITOR
            Destroy(this);
            return;
            #endif
            
            if (FindObjectOfType<Bootstrapper>())
                Destroy(this);
            else
                SceneManager.LoadScene(0);
        }
    }
}