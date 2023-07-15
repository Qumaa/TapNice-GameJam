using UnityEngine;

namespace Project.Configs
{
    [CreateAssetMenu(menuName = SOData.MenuPath.UI_CONFIG, fileName = SOData.FileName.UI_CONFIG)]
    public class UIConfig : ScriptableObject
    {
        [SerializeField] private GameObject _canvasPrefab;
        [SerializeField] private GameObject _menuUiPrefab;
        [SerializeField] private GameObject _gameUiPrefab;
        [SerializeField] private GameObject _pauseUiPrefab;

        public GameObject CanvasPrefab => _canvasPrefab;
        public GameObject MenuUiPrefab => _menuUiPrefab;
        public GameObject GameUiPrefab => _gameUiPrefab;
        public GameObject PauseUiPrefab => _pauseUiPrefab;
    }
}