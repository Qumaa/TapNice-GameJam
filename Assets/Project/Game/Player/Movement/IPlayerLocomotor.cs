namespace Project.Game
{
    public interface IPlayerLocomotor
    {
        public PlayerDirection Direction { get; set; }
        public IAffectable<float> JumpHeight { get; }
        public IAffectable<float> HorizontalSpeed { get; }
        void Jump();
        void UpdateHorizontalVelocity();
    }
}