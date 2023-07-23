using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Project.Game.ApplicationServices
{
    public class ApplicationQuitter : IApplicationQuitter
    {
        public void Quit()
        {
            #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            #endif
            
            Application.Quit();
        }
    }
}