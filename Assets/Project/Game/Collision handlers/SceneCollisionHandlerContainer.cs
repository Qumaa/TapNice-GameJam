using UnityEngine;

namespace Project.Game
{
    public class SceneCollisionHandlerContainer : MonoBehaviour, IPlayerCollisionHandler
    {
        private IPlayerCollisionHandler _underlyingHandler;
        public void HandleCollision(PlayerCollisionInfo collision) =>
            _underlyingHandler.HandleCollision(collision);

        public void SetHandler(IPlayerCollisionHandler handler) =>
            _underlyingHandler = handler;
    }
}