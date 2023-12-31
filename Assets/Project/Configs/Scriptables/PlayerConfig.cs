using UnityEngine;

namespace Project.Configs
{
    [CreateAssetMenu(menuName = SOData.MenuPath.PLAYER_CONFIG, fileName = SOData.FileName.PLAYER_CONFIG)]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _horizontalSpeed;
        [Header("Trail")]
        [SerializeField] private GameObject _trailPrefab;
        [SerializeField] private float _trailTime;
        [Header("Colors")]
        [SerializeField] private Color _playerColor;
        [SerializeField] private Color _canJumpPlayerColor;
        [SerializeField] private Color _finishColor;

        public GameObject PlayerPrefab => _playerPrefab;
        public float JumpHeight => _jumpHeight;
        public float HorizontalSpeed => _horizontalSpeed;
        public Color PlayerDefaultColor => _playerColor;
        public Color PlayerCanJumpColor => _canJumpPlayerColor;
        public GameObject TrailPrefab => _trailPrefab;
        public float TrailTime => _trailTime;

        public Color FinishColor => _finishColor;
    }
}