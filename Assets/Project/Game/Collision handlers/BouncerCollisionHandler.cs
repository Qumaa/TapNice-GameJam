namespace Project.Game.CollisionHandling
{
    public class BouncerCollisionHandler : ICollisionHandler
    {
        public void HandleCollision(PlayerCollisionInfo collision)
        {
            if (collision.IsOnWall)
                collision.Player.InvertDirection();
            
            collision.Player.Bounce();
        }
    }
}