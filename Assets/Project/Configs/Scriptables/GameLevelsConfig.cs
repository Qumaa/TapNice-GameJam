using Project.Game.Levels;
using UnityEngine;

namespace Project.Configs
{
    [CreateAssetMenu(menuName = SOData.MenuPath.LEVELS_CONFIG, fileName = SOData.FileName.LEVELS_CONFIG)]
    public class GameLevelsConfig : ScriptableObject
    {
        [SerializeField] private LevelDescriptor[] _levels;

        public LevelDescriptor[] Levels => _levels;
    }
}