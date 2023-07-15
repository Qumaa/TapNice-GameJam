using Project.Game.Levels;

namespace Project.Game.CollisionHandling
{
    public class FinishCollisionHandler : PlayerCollisionHandler
    {
        private readonly ILevel _level;

        public FinishCollisionHandler(ILevel level)
        {
            _level = level;
        }

        public override void HandleCollision(PlayerCollisionInfo collision)
        {
            if (collision.IsOnFloor)
                _level.Finish();
        }
    }
}