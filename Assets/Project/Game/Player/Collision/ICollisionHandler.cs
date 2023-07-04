namespace Project.Game
{
    public interface ICollisionHandler
    {
        static ICollisionHandler DefaultHandler;
        void HandleCollision(PlayerCollisionInfo collision);
    }
}