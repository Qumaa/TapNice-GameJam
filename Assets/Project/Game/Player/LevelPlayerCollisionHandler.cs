namespace Project.Game
{
    public class LevelPlayerCollisionHandler : PlayerCollisionHandler
    {
        private readonly float _jumpHeight;

        public LevelPlayerCollisionHandler(IPlayer player, float jumpHeight) : base(player)
        {
            _jumpHeight = jumpHeight;
        }

        public override void HandleCollision(PlayerCollisionInfo collision)
        {
            if (collision.IsVertical)
            {
                _player.ForceJump(_jumpHeight);
                _player.SetJumpingStatus(true);
                return;
            }

            _player.InvertDirection();
        }
    }
}