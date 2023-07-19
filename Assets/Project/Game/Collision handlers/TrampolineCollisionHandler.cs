using Project.Game.CollisionHandling;
using Project.Game.Effects;

namespace Project.Game
{
    public class TrampolineCollisionHandler : PlayerCollisionHandler
    {
        public override void HandleCollision(PlayerCollisionInfo collision)
        {
            if (collision.IsOnFloor)
                DoubleJumpHeightForOneJump(collision);

            DefaultHandling(collision);
        }

        private void DoubleJumpHeightForOneJump(PlayerCollisionInfo collision)
        {
            var player = collision.Player;

            player.JumpHeight.AddEffect(EffectFactory());
        }

        private static ScalePlayerEffect EffectFactory() =>
            new(0, 2);
    }
}