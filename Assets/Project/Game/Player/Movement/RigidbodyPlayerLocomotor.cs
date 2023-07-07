using UnityEngine;

namespace Project.Game
{
    public class RigidbodyPlayerLocomotor : IPlayerLocomotor
    {
        private readonly Rigidbody2D _rigidbody;
        private bool _isCurrentDirectionRight;
        private bool _isFrozen;
        private PlayerDirection _direction;

        public Vector2 Position
        {
            get => _rigidbody.position;
            set => _rigidbody.position = value;
        }

        public PlayerDirection Direction
        {
            get => _direction;
            set => SetDirection(value);
        }

        public IAffectable<float> JumpHeight { get; }

        public IAffectable<float> HorizontalSpeed { get; }

        public RigidbodyPlayerLocomotor(Rigidbody2D rigidbody, IAffectable<float> jumpHeight,
            IAffectable<float> horizontalSpeed)
        {
            _rigidbody = rigidbody;
            JumpHeight = jumpHeight;
            HorizontalSpeed = horizontalSpeed;

            _isFrozen = false;
        }

        public void UpdateHorizontalVelocity()
        {
            var vel = _rigidbody.velocity;
            var speed = HorizontalSpeed.AffectedValue;
            vel.x = IsDirectionRight(Direction) ? speed : -speed;
            _rigidbody.velocity = vel;
        }

        public void SetFrozen(bool frozen)
        {
            if (_isFrozen == frozen)
                return;

            _isFrozen = frozen;
            
            if (_isFrozen)
                Freeze();
            else
                Unfreeze();
        }

        public void Jump()
        {
            var vel = _rigidbody.velocity;
            vel.y = HeightToVelocity(JumpHeight.AffectedValue, Physics2D.gravity.y);
            _rigidbody.velocity = vel;

            UpdateHorizontalVelocity();
        }

        private float HeightToVelocity(float height, float gravity) =>
            _rigidbody.mass * Mathf.Sqrt(-2 * gravity * height);

        private void Freeze()
        {
            _isFrozen = true;

            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        private void Unfreeze()
        {
            _isFrozen = false;

            _rigidbody.constraints = RigidbodyConstraints2D.None;
        }

        private static bool IsDirectionRight(PlayerDirection direction) =>
            direction == PlayerDirection.Right;

        private void SetDirection(PlayerDirection direction)
        {
            _direction = direction;
            UpdateHorizontalVelocity();
        }
    }
}