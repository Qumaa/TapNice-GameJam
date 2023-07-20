using Project.Game.Effects;

namespace Project.Game.CollisionHandling
{
    public class DischargerCollisionHandler : PlayerCollisionHandler
    {
        public override void HandleCollision(PlayerCollisionInfo collision)
        {
            DefaultHandling(collision);
            collision.Player.CanJump = false;
            collision.Player.Color.AddEffect(new DischargedPlayerColorEffect());
        }
    }
}