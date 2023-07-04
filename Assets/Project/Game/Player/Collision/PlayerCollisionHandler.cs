namespace Project.Game
{
    public abstract class PlayerCollisionHandler : IPlayerCollisionHandler
    {
        protected readonly IPlayer _player;

        protected PlayerCollisionHandler(IPlayer player)
        {
            _player = player;
        }

        public abstract void HandleCollision(PlayerCollisionInfo collision);

        protected void DefaultHandling(PlayerCollisionInfo collision)
        {
            if (collision.IsVertical)
            {
                _player.Jump();
                _player.Bounce();
                _player.CanJump = true;
                return;
            }

            _player.InvertDirection();
        }
    }
}