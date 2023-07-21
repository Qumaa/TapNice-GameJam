namespace Project.Game.CollisionHandling
{
    public class BouncerCollisionHandler : PlayerCollisionHandler
    {
        public override void HandleCollision(PlayerCollisionInfo collision)
        {
            TryHandleWall(collision);
            
            collision.Player.Bounce();
            PlayRippleEffect(collision);
        }
    }
}