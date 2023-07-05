namespace Project.Game
{
    public class FinishCollisionHandler : PlayerCollisionHandler
    {
        public override void HandleCollision(PlayerCollisionInfo collision)
        {
            var player = collision.Player;

            var effect = new ScalePlayerEffect(0, 2);
            player.JumpHeight.AddEffect(effect);
            
            DefaultHandling(collision);
            // Debug.Log("Finish");
        }
    }
}