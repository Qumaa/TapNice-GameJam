using System;

namespace Project.Game
{
    public class LevelCollisionHandler : PlayerCollisionHandler
    {
        public LevelCollisionHandler(IPlayer player) : base(player)
        {
        }

        public override void HandleCollision(PlayerCollisionInfo collision)
        {
            DefaultHandling(collision);
        }
    }
}