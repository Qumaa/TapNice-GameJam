using System;
using UnityEngine;

namespace Project.Game
{
    public interface IPlayer
    {
        public Affectable<float> JumpHeight { get; set; }
        public float HorizontalSpeed { get; set; }
        public bool CanJump { get; set; }
        public PlayerDirection Direction { get; set; }

        event Action<PlayerCollisionInfo> OnCollided;
        event Action OnJumped;
        event Action OnBounced;

        /// <summary>
        /// Call this method when player gives an input.
        /// This method invokes <see cref="OnJumped"/> event.
        /// </summary>
        void Jump();

        /// <summary>
        /// Call this method when player hits the floor.
        /// This method will invoke <see cref="OnBounced"/>, jump without invoking <see cref="OnJumped"/>
        /// and set <see cref="CanJump"/> to true.
        /// </summary>
        void Bounce();

        void InvertDirection();
        void SetColors(Color cantJump, Color canJump);
    }
}