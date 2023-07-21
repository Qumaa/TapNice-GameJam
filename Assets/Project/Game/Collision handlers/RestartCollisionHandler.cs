using Project.Game.Levels;

namespace Project.Game.CollisionHandling
{
    public class RestartCollisionHandler : PlayerCollisionHandler
    {
        private readonly ILevel _level;

        public RestartCollisionHandler(ILevel level)
        {
            _level = level;
        }

        public override void HandleCollision(PlayerCollisionInfo collision) =>
            _level.Restart();
    }
}