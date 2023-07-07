using System;
using UnityEngine;

namespace Project.Game
{
    public interface IPlayer : IActivatable
    {
        public IAffectable<float> JumpHeight { get; }
        public IAffectable<float> HorizontalSpeed { get; }
        public bool CanJump { get; set; }
        public PlayerDirection Direction { get; set; }

        event Action<PlayerCollisionInfo> OnCollided;
        event Action OnJumped;
        event Action OnBounced;
        event Action<bool> OnCanJumpChanged;

        /// <summary>
        /// Call this method when player gives an input.
        /// This method invokes the <see cref="OnJumped"/> event.
        /// </summary>
        void Jump();

        /// <summary>
        /// Call this method when player hits the floor.
        /// This method will invoke <see cref="OnBounced"/>, jump without invoking <see cref="OnJumped"/>
        /// and set <see cref="CanJump"/> to true.
        /// </summary>
        void Bounce();

        void InvertDirection();
        void HandleCollision(Collision2D other);

        /// <summary>
        /// This method is called whenever a level is loaded and player have to be positioned on it.
        /// </summary>
        void Reset(Vector2 position, PlayerDirection direction);
    }
}