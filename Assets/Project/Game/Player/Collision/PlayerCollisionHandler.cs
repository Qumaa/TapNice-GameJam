namespace Project.Game.CollisionHandling
{
    public abstract class PlayerCollisionHandler : ICollisionHandler
    {
        public abstract void HandleCollision(PlayerCollisionInfo collision);

        protected static void DefaultHandling(PlayerCollisionInfo collision)
        {
            if (TryHandleFloor(collision))
            {
                PlayRippleEffect(collision);
                return;
            }

            if (TryHandleWall(collision))
            {
                PlayRippleEffect(collision);
                return;
            }

            collision.Player.UpdateHorizontalVelocity();
        }

        protected static bool TryHandleWall(PlayerCollisionInfo collision)
        {
            if (!collision.IsOnWall)
                return false;
            
            collision.Player.InvertDirection();
            return true;

        }

        protected static bool TryHandleFloor(PlayerCollisionInfo collision)
        {
            if (!collision.IsOnFloor)
                return false;
            
            collision.Player.Bounce();
            return true;
        }

        protected static void PlayRippleEffect(PlayerCollisionInfo collision) =>
            collision.Player.PlayRippleEffect();
    }
}