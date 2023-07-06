namespace Project.Game
{
    public class DischargerCollisionHandler : PlayerCollisionHandler
    {
        public override void HandleCollision(PlayerCollisionInfo collision)
        {
            DefaultHandling(collision);
            collision.Player.CanJump = false;
        }
    }
}