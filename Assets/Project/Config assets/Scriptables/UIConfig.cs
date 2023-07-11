using UnityEngine;

namespace Project.Game
{
    [CreateAssetMenu(menuName = SOData.MenuPath.UI_CONFIG, fileName = SOData.FileName.UI_CONFIG)]
    public class UIConfig : ScriptableObject
    {
        [SerializeField] private GameObject _canvasPrefab;
        [SerializeField] private GameObject _menuUiPrefab;
        [SerializeField] private GameObject _gameUiPrefab;

        public GameObject CanvasPrefab => _canvasPrefab;
        public GameObject MenuUiPrefab => _menuUiPrefab;
        public GameObject GameUiPrefab => _gameUiPrefab;
    }
}