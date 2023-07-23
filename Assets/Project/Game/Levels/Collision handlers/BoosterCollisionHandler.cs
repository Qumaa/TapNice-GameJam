using Project.Game.Effects;

namespace Project.Game.CollisionHandling
{
    public class BoosterCollisionHandler : PlayerCollisionHandler
    {
        public override void HandleCollision(PlayerCollisionInfo collision)
        {
            if (collision.IsOnFloor)
                collision.Player.HorizontalSpeed.AddEffect(new ScalePlayerEffect(0, 2));

            DefaultHandling(collision);
        }
    }
}