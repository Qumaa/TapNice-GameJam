namespace Project.Game
{
    public class FinishCollisionHandler : PlayerCollisionHandler
    {
        private readonly IEffectsManager _effectsManager;

        public FinishCollisionHandler(IEffectsManager effectsManager)
        {
            _effectsManager = effectsManager;
        }

        public override void HandleCollision(PlayerCollisionInfo collision)
        {
            var player = collision.Player;

            var effect = new ScalePlayerEffect(0, 2);
            player.JumpHeight.AddEffect(effect);
            _effectsManager.AddEffect(effect);
            
            DefaultHandling(collision);
            // Debug.Log("Finish");
        }
    }
}