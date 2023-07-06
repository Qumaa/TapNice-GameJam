using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Game
{
    public class LevelDescriptor : ScriptableObject
    {
        [SerializeField] private Scene _targetScene;
        [SerializeField] private string _name;
    }
}
