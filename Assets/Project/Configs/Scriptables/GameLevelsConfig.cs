using UnityEngine;

namespace Project.Game
{
    [CreateAssetMenu(menuName = SOData.MenuPath.LEVELS_CONFIG, fileName = SOData.FileName.LEVELS_CONFIG)]
    public class GameLevelsConfig : ScriptableObject
    {
        [SerializeField] private LevelDescriptor[] _levels;

        public LevelDescriptor[] Levels => _levels;
    }
}