namespace Project.Game
{
    public class FinishCollisionHandler : PlayerCollisionHandler
    {
        private readonly ILevel _level;

        public FinishCollisionHandler(ILevel level)
        {
            _level = level;
        }

        public override void HandleCollision(PlayerCollisionInfo _)
        {
            _level.Finish();
        }
    }
}