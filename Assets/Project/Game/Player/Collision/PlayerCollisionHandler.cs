namespace Project.Game
{
    public abstract class PlayerCollisionHandler : ICollisionHandler
    {
        public abstract void HandleCollision(PlayerCollisionInfo collision);

        protected static void DefaultHandling(PlayerCollisionInfo collision)
        {
            var player = collision.Player;
            
            if (collision.IsOnFloor)
            {
                player.Bounce();
                return;
            }

            player.InvertDirection();
            
        }
    }
}