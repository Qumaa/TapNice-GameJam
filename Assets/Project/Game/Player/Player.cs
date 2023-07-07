using System;
using UnityEngine;

namespace Project.Game
{
    public class Player : IPlayer
    {
        private readonly GameObject _gameObject;
        private readonly IPlayerLocomotor _locomotor;
        private readonly IPlayerColors _colors;
        private readonly IPlayerInputService _inputService;
        private readonly ICollisionDetector _collisionDetector;
        private bool _canJump;
        private Action _jumpStrategy;

        public IAffectable<float> JumpHeight => _locomotor.JumpHeight;
        public IAffectable<float> HorizontalSpeed => _locomotor.HorizontalSpeed;

        public bool CanJump
        {
            get => _canJump;
            set => SetJumpingStatus(value);
        }

        public PlayerDirection Direction
        {
            get => _locomotor.Direction;
            set => _locomotor.Direction = value;
        }

        public event Action<PlayerCollisionInfo> OnCollided;
        public event Action OnJumped;
        public event Action OnBounced;

        public Player(GameObject gameObject, IPlayerLocomotor locomotor, IPlayerColors colors,
            IPlayerInputService inputService, ICollisionDetector collisionDetector)
        {
            _gameObject = gameObject;
            _locomotor = locomotor;
            _colors = colors;
            _inputService = inputService;
            _collisionDetector = collisionDetector;
            
            _inputService.OnJumpInput += this.JumpIfPossible;
            _collisionDetector.OnCollided += HandleCollision;

            _jumpStrategy = InitialJump;
        }

        public void Jump()
        {
            _jumpStrategy();
            OnJumped?.Invoke();
        }

        public void Bounce()
        {
            _locomotor.Jump();
            CanJump = true;
            OnBounced?.Invoke();
        }

        public void InvertDirection() =>
            Direction = Direction == PlayerDirection.Right ? PlayerDirection.Left : PlayerDirection.Right;

        private void HandleCollision(Collision2D other)
        {
            var handler = other.gameObject.GetComponent<ICollisionHandler>();

            var info = new PlayerCollisionInfo(other, Vector2.up, handler, this);

            OnCollided?.Invoke(info);
            handler.HandleCollision(info);
        }

        public void Reset(Vector2 position, PlayerDirection direction)
        {
            _locomotor.Position = position;
            _locomotor.Direction = direction;
            
            _locomotor.SetFrozen(true);
            _canJump = true;
            _jumpStrategy = InitialJump;
        }

        public void Activate()
        {
            // _gameObject.SetActive(true);
            _locomotor.Activate();
            _colors.Activate();
            _inputService.OnJumpInput += this.JumpIfPossible;
            _collisionDetector.OnCollided += HandleCollision;
        }

        public void Deactivate()
        {
            // _gameObject.SetActive(false);
            _locomotor.Deactivate();
            _colors.Deactivate();
            _inputService.OnJumpInput -= this.JumpIfPossible;
            _collisionDetector.OnCollided -= HandleCollision;
        }

        private void InitialJump()
        {
            _locomotor.SetFrozen(false);
            _locomotor.UpdateHorizontalVelocity();
            _jumpStrategy = NormalJump;
        }

        private void NormalJump() =>
            _locomotor.Jump();

        private void SetJumpingStatus(bool status)
        {
            if (_canJump == status)
                return;
            
            _canJump = status;
            _colors.UpdateColors(_canJump);
        }
    }
}