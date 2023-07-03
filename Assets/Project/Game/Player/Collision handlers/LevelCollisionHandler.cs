using System;

namespace Project.Game
{
    public class LevelCollisionHandler : PlayerCollisionHandler
    {
        private readonly float _jumpHeight;

        public LevelCollisionHandler(IPlayer player, float jumpHeight) : base(player)
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