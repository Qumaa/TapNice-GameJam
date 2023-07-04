using UnityEngine;

namespace Project.Game
{
    public class SceneCollisionHandlerContainer : MonoBehaviour, ICollisionHandler
    {
        [SerializeField] private CollisionHandlerType _handlerType;

        public CollisionHandlerType HandlerType => _handlerType;

        private ICollisionHandler _underlyingHandler;
        public void HandleCollision(PlayerCollisionInfo collision) =>
            _underlyingHandler.HandleCollision(collision);

        public void SetHandler(ICollisionHandler handler) =>
            _underlyingHandler = handler;
    }
}