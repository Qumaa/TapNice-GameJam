using UnityEngine;

namespace Project.Game.Levels
{
    [CreateAssetMenu(menuName = SOData.MenuPath.LEVEL_DESCRIPTOR, fileName = SOData.FileName.LEVEL_DESCRIPTOR)]
    public class LevelDescriptor : ScriptableObject, ILevelDescriptor
    {
        // !!! DO NOT !!! change this, or change the respective constant in SceneDescriptorInspector afterwards
        [SerializeField] private SceneData _targetScene;
        [SerializeField] private string _name;
        [SerializeField] private bool _unlockedByDefault;

        public int SceneIndex => _targetScene.BuildIndex;
        public string SceneName => _targetScene.Name;
        public string LevelName => _name;
        public bool UnlockedByDefault => _unlockedByDefault;
    }
}
