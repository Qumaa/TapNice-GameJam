using System;
using UnityEngine;

namespace Project.Game
{
    public class Player : MonoBehaviour, IPlayer
    {
        private IPlayerInputService _inputService;
        private SpriteRenderer _renderer;
        private Rigidbody2D _rigidbody;
        private Color _playerDefaultColor;
        private Color _playerCanJumpColor;
        private bool _isCurrentDirectionRight;
        private bool _canJump;
        private bool _hasCollided;
        private PlayerDirection _direction;

        public Affectable<float> JumpHeight { get; set; }
        public float HorizontalSpeed { get; set; }

        public bool CanJump
        {
            get => _canJump;
            set => SetJumpingStatus(value);
        }

        public PlayerDirection Direction
        {
            get => _direction;
            set => SetDirection(value);
        }

        public event Action<PlayerCollisionInfo> OnCollided;
        public event Action OnJumped;
        public event Action OnBounced;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();

            _inputService = GetComponent<LegacyInputService>();
            _inputService.OnJumpInput += TryJumpInternal;
        }

        private void Start()
        {
            UpdateHorizontalVelocity();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_hasCollided)
                return;

            _hasCollided = true;

            var handler = other.gameObject.GetComponent<ICollisionHandler>();

            var info = new PlayerCollisionInfo(other, Vector2.up, handler, this);

            OnCollided?.Invoke(info);
            handler.HandleCollision(info);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            _hasCollided = false;
        }

        public void Jump()
        {
            JumpInternal();
            OnJumped?.Invoke();
        }

        public void Bounce()
        {
            JumpInternal();
            CanJump = true;
            OnBounced?.Invoke();
        }

        public void InvertDirection() =>
            SetDirectionInternal(GetInvertedDirection());

        public void SetColors(Color cantJump, Color canJump)
        {
            _playerDefaultColor = cantJump;
            _playerCanJumpColor = canJump;
            UpdateColors();
        }

        private void SetJumpingStatus(bool status)
        {
            _canJump = status;
            UpdateColors();
        }

        private void SetDirection(PlayerDirection direction)
        {
            _direction = direction;
            SetDirectionInternal(PlayerDirectionToInternalBool(direction));
        }

        private void TryJumpInternal()
        {
            if (this.TryJump())
                CanJump = false;
        }

        private void JumpInternal()
        {
            var vel = _rigidbody.velocity;
            vel.y = HeightToVelocity(JumpHeight, Physics2D.gravity.y);
            _rigidbody.velocity = vel;

            UpdateHorizontalVelocity();
        }

        private void SetDirectionInternal(bool right)
        {
            _isCurrentDirectionRight = right;
            UpdateHorizontalVelocity();
        }

        private void UpdateColors()
        {
            var color = CanJump ? _playerCanJumpColor : _playerDefaultColor;

            _renderer.color = color;
        }

        private bool GetInvertedDirection() =>
            !_isCurrentDirectionRight;

        private void UpdateHorizontalVelocity()
        {
            var vel = _rigidbody.velocity;
            vel.x = _isCurrentDirectionRight ? HorizontalSpeed : -HorizontalSpeed;
            _rigidbody.velocity = vel;
        }

        private static float HeightToVelocity(float height, float gravity) =>
            Mathf.Sqrt(-2 * gravity * height);

        private static bool PlayerDirectionToInternalBool(PlayerDirection direction) =>
            direction == PlayerDirection.Right;
    }
}