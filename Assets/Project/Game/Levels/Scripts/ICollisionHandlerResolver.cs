namespace Project.Game.CollisionHandling
{
    public interface ICollisionHandlerResolver
    {
        ICollisionHandler Resolve(CollisionHandlerType type);
    }
}