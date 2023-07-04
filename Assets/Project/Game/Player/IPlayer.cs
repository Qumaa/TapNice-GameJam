using System;
using UnityEngine;

namespace Project.Game
{
    public interface IPlayer
    {
        public float JumpHeight { get; set; }
        public float HorizontalSpeed { get; set; }
        public bool CanJump { get; set; }
        public PlayerDirection Direction { get; set; }

        event Action<PlayerCollisionInfo> OnCollided;
        event Action OnJumped;
        event Action OnBounced;

        void Jump();

        /// <summary>
        /// Call this method when player hits the floor.
        /// This method should only invoke <see cref="OnBounced"/>
        /// </summary>
        void Bounce();

        void InvertDirection();
        void SetColors(Color cantJump, Color canJump);
    }
}