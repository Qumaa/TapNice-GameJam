namespace Project.Game.CollisionHandling
{
    public class BouncerCollisionHandler : PlayerCollisionHandler
    {
        public override void HandleCollision(PlayerCollisionInfo collision)
        {
            if (collision.IsOnWall)
                collision.Player.InvertDirection();
            
            collision.Player.Bounce();
        }
    }
}