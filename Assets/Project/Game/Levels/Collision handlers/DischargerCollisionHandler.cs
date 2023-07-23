using Project.Game.Effects;

namespace Project.Game.CollisionHandling
{
    public class DischargerCollisionHandler : PlayerCollisionHandler
    {
        public override void HandleCollision(PlayerCollisionInfo collision)
        {
            collision.Player.Color.AddEffect(new DischargerColorEffect());
            DefaultHandling(collision);
            collision.Player.CanJump = false;
        }
    }
}