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
    }
}