using UnityEngine;

namespace Project.Game
{
    public class SceneCollisionHandlerContainer : MonoBehaviour, ICollisionHandler, ICollisionHandlerContainer
    {
        [SerializeField] private CollisionHandlerType _handlerType;

        public CollisionHandlerType HandlerType => _handlerType;

        private ICollisionHandler _underlyingHandler;
        public void HandleCollision(PlayerCollisionInfo collision) =>
            _underlyingHandler.HandleCollision(collision);

        public void SetHandler(ICollisionHandler handler) =>
            _underlyingHandler = handler;
    }

    public interface ICollisionHandlerContainer
    {
        CollisionHandlerType HandlerType { get; }
        void SetHandler(ICollisionHandler handler);
    }
}