using System;
using UnityEngine;

namespace Project.Game
{
    public class Player : MonoBehaviour, IPlayer
    {
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _horizontalVelocity;
        [SerializeField] private PlayerDirection _initialDirection;
        [SerializeField] private Color _playerColor;
        [SerializeField] private Color _canJumpPlayerColor;
        [SerializeField] private TrailRenderer _trailRenderer;

        private Rigidbody2D _rigidbody;
        private SpriteRenderer _renderer;

        private bool _isCurrentDirectionRight;
        private bool _canJump;

        private IPlayerCollisionHandler _defaultHandler;
        private IPlayerInputService _inputService;

        public event Action<PlayerCollisionInfo> OnCollided;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();
            SetDirection(_initialDirection);

            _defaultHandler = new LevelCollisionHandler(this, _jumpHeight);

            _inputService = GetComponent<LegacyInputService>();
            _inputService.OnJumpInput += TryJumpInternal;

            InitializeFinishHandlers();
        }

        private void InitializeFinishHandlers()
        {
            var sceneHandlers =
                FindObjectsByType<SceneCollisionHandler>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            
            if (sceneHandlers.Length == 0)
                return;
            
            var handler = new FinishCollisionHandler(this);
            
            foreach(var sceneHandler in sceneHandlers)
                sceneHandler.Initialize(handler);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent<IPlayerCollisionHandler>(out var handler))
                handler = _defaultHandler;
            
            var info = new PlayerCollisionInfo(other, Vector2.up, handler, this);
            
            OnCollided?.Invoke(info);
            handler.HandleCollision(info);
        }

        public void SetJumpingStatus(bool status)
        {
            _canJump = status;
            UpdateColor();
        }

        public bool TryJump(float height)
        {
            if (!_canJump)
                return false;

            JumpInternal(height);
            SetJumpingStatus(false);
            return true;
        }

        public void ForceJump(float height) =>
            JumpInternal(height);

        public void InvertDirection() =>
            SetDirectionInternal(GetInvertedDirection());

        public void SetDirection(PlayerDirection direction) =>
            SetDirectionInternal(PlayerDirectionToInternalBool(direction));

        private void UpdateColor()
        {
            var color = _canJump ? _canJumpPlayerColor : _playerColor;

            _trailRenderer.startColor = _trailRenderer.endColor = _renderer.color = color;
        }

        private void JumpInternal(float height)
        {
            var vel = _rigidbody.velocity;
            vel.y = HeightToVelocity(height, Physics2D.gravity.y);
            _rigidbody.velocity = vel;

            UpdateHorizontalVelocity();
        }

        private void SetDirectionInternal(bool right)
        {
            _isCurrentDirectionRight = right;
            UpdateHorizontalVelocity();
        }

        private bool GetInvertedDirection() =>
            !_isCurrentDirectionRight;

        private void UpdateHorizontalVelocity()
        {
            var vel = _rigidbody.velocity;
            vel.x = _isCurrentDirectionRight ? _horizontalVelocity : -_horizontalVelocity;
            _rigidbody.velocity = vel;
        }

        private void TryJumpInternal() =>
            TryJump(_jumpHeight);

        private static float HeightToVelocity(float height, float gravity) =>
            Mathf.Sqrt(-2 * gravity * height);

        private static bool PlayerDirectionToInternalBool(PlayerDirection direction) =>
            direction == PlayerDirection.Right;
    }
}