using System;
using UnityEngine;

namespace Project.Game
{
    public class Player : MonoBehaviour, IPlayer
    {
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _horizontalVelocity;

        private Rigidbody2D _rigidbody;

        private bool _isCurrentDirectionRight;
        private bool _canJump;

        private IPlayerCollisionHandler _defaultHandler;

        public void SetJumpingStatus(bool status) =>
            _canJump = status;

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
            SetDirectionInternal(direction == PlayerDirection.Right);

        public event Action<PlayerCollisionInfo> OnCollided;

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

        private void UpdateHorizontalVelocity()
        {
            var vel = _rigidbody.velocity;
            vel.x = _isCurrentDirectionRight ? _horizontalVelocity : -_horizontalVelocity;
            _rigidbody.velocity = vel;
        }

        private bool GetInvertedDirection() =>
            !_isCurrentDirectionRight;

        private static float HeightToVelocity(float height, float gravity) =>
            Mathf.Sqrt(-2 * gravity * height);

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnCollided?.Invoke(new PlayerCollisionInfo(other, Vector2.up, _defaultHandler));
        }

        private void Update()
        {
            if (!Input.anyKeyDown)
                return;

            TryJump(_jumpHeight);
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            UpdateHorizontalVelocity();

            _defaultHandler = new LevelPlayerCollisionHandler(this, _jumpHeight);
            OnCollided += _defaultHandler.HandleCollision;
        }
    }
}