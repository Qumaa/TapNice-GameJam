using System;
using Project.Game.CollisionHandling;
using Project.Game.Effects;
using UnityEngine;

namespace Project.Game.Player
{
    public interface IPlayer : IActivatable
    {
        public IAffectable<float> JumpHeight { get; }
        public IAffectable<float> HorizontalSpeed { get; }
        public IAffectable<Color> Color { get; }
        public bool CanJump { get; set; }
        public PlayerDirection Direction { get; set; }

        event Action<PlayerCollisionInfo> OnCollided;
        event Action OnJumped;
        event Action OnBounced;

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

        void UpdateHorizontalVelocity();
        /// <summary>
        /// This method is called whenever a level is loaded and player have to be positioned on it.
        /// </summary>
        void Reset(Vector2 position, PlayerDirection direction);
    }

    public static class PlayerExtensions
    {
        /// <summary>
        /// If player's flag <see cref="IPlayer.CanJump"/> is true, calls <see cref="IPlayer.Jump"/> and sets the flag to false.
        /// </summary>
        public static bool JumpIfPossible(this IPlayer player)
        {
            if (!player.CanJump)
                return false;

            player.Jump();
            player.CanJump = false;
            return true;
        }

        /// <summary>
        /// A variation of <see cref="IPlayer.Reset"/> method when parameters are unknown.
        /// </summary>
        public static void Reset(this IPlayer player) =>
            player.Reset(Vector2.zero, PlayerDirection.Left);
    }
}