namespace Project.Game
{
    public class FinishCollisionHandler : PlayerCollisionHandler
    {
        private readonly IContainer<ScalePlayerEffect> _container = new EffectsContainer<ScalePlayerEffect>(EffectFactory);

        public override void HandleCollision(PlayerCollisionInfo collision)
        {
            var player = collision.Player;

            var effect = _container.Resolve();
            player.JumpHeight.AddEffect(effect);
            
            DefaultHandling(collision);
        }

        private static ScalePlayerEffect EffectFactory() =>
            new(0, 2);
    }
}