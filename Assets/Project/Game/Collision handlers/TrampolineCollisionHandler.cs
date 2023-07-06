namespace Project.Game
{
    public class TrampolineCollisionHandler : PlayerCollisionHandler
    {
        private readonly IContainer<ScalePlayerEffect> _container = new EffectsContainer<ScalePlayerEffect>(EffectFactory);

        public override void HandleCollision(PlayerCollisionInfo collision)
        {
            if (collision.IsOnFloor)
                DoubleJumpHeightForOneJump(collision);

            DefaultHandling(collision);
        }

        private void DoubleJumpHeightForOneJump(PlayerCollisionInfo collision)
        {
            var player = collision.Player;

            var effect = _container.Resolve();
            player.JumpHeight.AddEffect(effect);
        }

        private static ScalePlayerEffect EffectFactory() =>
            new(0, 2);
    }
}