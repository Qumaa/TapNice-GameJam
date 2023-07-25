using System;
using Project.Game.CollisionHandling;
using Project.Game.Effects;
using Project.Game.Player.VFX;
using UnityEngine;

namespace Project.Game.Player
{
    public class PlayerScript : IPlayer
    {
        private readonly IPlayerLocomotor _locomotor;
        private readonly IPlayerColors _colors;
        private readonly ICollisionDetector _collisionDetector;
        private readonly IPlayerTrail _trail;
        private readonly IPlayerRippleVFX _rippleVFX;
        private bool _canJump;
        private Action _jumpStrategy;

        public IAffectable<float> JumpHeight => _locomotor.JumpHeight;
        public IAffectable<float> HorizontalSpeed => _locomotor.HorizontalSpeed;
        public IAffectable<Color> Color => _colors.Color;

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

        public PlayerScript(IPlayerLocomotor locomotor, IPlayerColors colors, ICollisionDetector collisionDetector, IPlayerTrail trail,
            IPlayerRippleVFX rippleVFX)
        {
            _locomotor = locomotor;
            _colors = colors;
            _collisionDetector = collisionDetector;
            _trail = trail;
            _rippleVFX = rippleVFX;

            _collisionDetector.OnCollided += HandleCollision;

            _jumpStrategy = InitialJump;
        }

        public void Jump()
        {
            _jumpStrategy();
            PlayRippleEffect();
            Debug.Break();
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

        public void UpdateHorizontalVelocity() =>
            _locomotor.UpdateHorizontalVelocity();
        
        private void HandleCollision(CustomCollision2D other)
        {
            var handler = other.OtherObject.GetComponent<ICollisionHandler>();

            var info = new PlayerCollisionInfo(other.Contact, this);

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

            _collisionDetector.Reset();
            _trail.Reset();
            _rippleVFX.Reset();
        }

        public void Activate()
        {
            _colors.Activate();
            _trail.Activate();
            _collisionDetector.OnCollided += HandleCollision;
        }

        public void Deactivate()
        {
            _locomotor.Reset();
            _colors.Deactivate();
            _trail.Deactivate();
            _collisionDetector.Reset();
            _collisionDetector.OnCollided -= HandleCollision;
            _rippleVFX.Reset();
        }

        private void InitialJump()
        {
            _locomotor.SetFrozen(false);
            _locomotor.UpdateHorizontalVelocity();
            _jumpStrategy = NormalJump;
        }

        public void PlayRippleEffect() =>
            _rippleVFX.PlayRipple(_locomotor.Position, Color.AffectedValue);

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