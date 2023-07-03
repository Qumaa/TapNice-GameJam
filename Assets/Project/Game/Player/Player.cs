using System;
using UnityEngine;

namespace Project
{
    public class Player : MonoBehaviour, IPlayer
    {
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _horizontalVelocity;

        private Rigidbody2D _rigidbody;
        
        private bool _isCurrentDirectionRight;
        private bool _canJump;

        public void SetJumpingStatus(bool status) =>
            _canJump = status;

        public bool TryJump(float height)
        {
            if (!_canJump)
                return false;

            JumpInternal(height);
            return true;
        }

        public void InvertDirection() =>
            SetDirectionInternal(GetInvertedDirection());

        public void SetDirection(PlayerDirection direction) =>
            SetDirectionInternal(direction == PlayerDirection.Right);

        private void JumpInternal(float height)
        {
            var vel = _rigidbody.velocity;
            vel.y = HeightToVelocity(height, Physics2D.gravity.y);
            _rigidbody.velocity = vel;
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
            var normal = other.contacts[0].normal.normalized;
            
            if (normal.y == 1)
            {
                JumpInternal(_jumpHeight);
                SetJumpingStatus(true);
                UpdateHorizontalVelocity();
                return;
            }

            if (Mathf.Abs(normal.x) == 1)
            {
                InvertDirection();
                UpdateHorizontalVelocity();
                return;
            }
        }

        private void Update()
        {
            if (!Input.anyKeyDown)
                return;
            
            if (!TryJump(_jumpHeight))
                Debug.Log("Unable to jump");
            else
                SetJumpingStatus(false);
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            UpdateHorizontalVelocity();
        }
    }
}