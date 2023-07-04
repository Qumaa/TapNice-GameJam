namespace Project.Game
{
    public abstract class PlayerCollisionHandler : ICollisionHandler
    {
        private static ICollisionHandler _defaultHandler;

        public static ICollisionHandler DefaultHandler => _defaultHandler ??= new LevelCollisionHandler();
        
        public abstract void HandleCollision(PlayerCollisionInfo collision);

        protected static void DefaultHandling(PlayerCollisionInfo collision)
        {
            var player = collision.Player;
            
            if (collision.IsVertical)
            {
                player.Jump();
                player.Bounce();
                player.CanJump = true;
                return;
            }

            player.InvertDirection();
        }
    }
}