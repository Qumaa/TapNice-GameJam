using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    [CreateAssetMenu(menuName = AssetMenuPaths.PLAYER_CONFIG, fileName = "Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private Color _playerColor;
        [SerializeField] private Color _canJumpPlayerColor;

        public GameObject PlayerPrefab => _playerPrefab.gameObject;
        public float JumpHeight => _jumpHeight;
        public float HorizontalSpeed => _horizontalSpeed;

        public Color PlayerDefaultColor => _playerColor;

        public Color PlayerCanJumpColor => _canJumpPlayerColor;
    }
}