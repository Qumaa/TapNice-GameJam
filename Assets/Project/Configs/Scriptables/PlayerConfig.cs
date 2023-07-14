using UnityEngine;

namespace Project.Game
{
    [CreateAssetMenu(menuName = SOData.MenuPath.PLAYER_CONFIG, fileName = SOData.FileName.PLAYER_CONFIG)]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private Color _playerColor;
        [SerializeField] private Color _canJumpPlayerColor;
        [Header("Trail")]
        [SerializeField] private GameObject _trailPrefab;
        [SerializeField] private float _trailTime;

        public GameObject PlayerPrefab => _playerPrefab;
        public float JumpHeight => _jumpHeight;
        public float HorizontalSpeed => _horizontalSpeed;
        public Color PlayerDefaultColor => _playerColor;
        public Color PlayerCanJumpColor => _canJumpPlayerColor;
        public GameObject TrailPrefab => _trailPrefab;
        public float TrailTime => _trailTime;
    }
}