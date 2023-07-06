using System;
using UnityEngine;

namespace Project.Game
{
    public class Player : IPlayer
    {
        private readonly IPlayerLocomotor _playerLocomotor;
        private bool _canJump;

        public IAffectable<float> JumpHeight => _playerLocomotor.JumpHeight;
        public IAffectable<float> HorizontalSpeed => _playerLocomotor.HorizontalSpeed;

        public bool CanJump
        {
            get => _canJump;
            set => SetJumpingStatus(value);
        }

        public PlayerDirection Direction
        {
            get => _playerLocomotor.Direction;
            set => _playerLocomotor.Direction = value;
        }

        public event Action<PlayerCollisionInfo> OnCollided;
        public event Action OnJumped;
        public event Action OnBounced;
        public event Action<bool> OnCanJumpChanged; 

        public Player(IPlayerLocomotor playerLocomotor)
        {
            _playerLocomotor = playerLocomotor;
            _playerLocomotor.UpdateHorizontalVelocity();
        }

        public void Jump()
        {
            _playerLocomotor.Jump();
            OnJumped?.Invoke();
        }

        public void Bounce()
        {
            _playerLocomotor.Jump();
            CanJump = true;
            OnBounced?.Invoke();
        }

        public void InvertDirection() =>
            Direction = Direction == PlayerDirection.Right ? PlayerDirection.Left : PlayerDirection.Right;

        private void SetJumpingStatus(bool status)
        {
            if (_canJump == status)
                return;
            
            _canJump = status;
            OnCanJumpChanged?.Invoke(_canJump);
        }

        public void HandleCollision(Collision2D other)
        {
            var handler = other.gameObject.GetComponent<ICollisionHandler>();

            var info = new PlayerCollisionInfo(other, Vector2.up, handler, this);

            OnCollided?.Invoke(info);
            handler.HandleCollision(info);
        }
    }
}