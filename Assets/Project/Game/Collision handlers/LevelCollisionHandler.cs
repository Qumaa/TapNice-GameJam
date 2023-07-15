using Project.Game.CollisionHandling;

namespace Project.Game
{
    public class LevelCollisionHandler : PlayerCollisionHandler
    {
        public override void HandleCollision(PlayerCollisionInfo collision)
        {
            DefaultHandling(collision);
        }
    }
}