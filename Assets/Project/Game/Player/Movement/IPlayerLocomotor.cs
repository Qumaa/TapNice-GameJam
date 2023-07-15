using Project.Game.Effects;
using UnityEngine;

namespace Project.Game.Player
{
    public interface IPlayerLocomotor : IResettable
    {
        public Vector2 Position { get; set; }
        public PlayerDirection Direction { get; set; }
        public IAffectable<float> JumpHeight { get; }
        public IAffectable<float> HorizontalSpeed { get; }
        void Jump();
        void UpdateHorizontalVelocity();
        void SetFrozen(bool frozen);
    }
}