using UnityEngine;

namespace Project.Game
{
    public interface IPlayerLocomotor : IActivatable
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