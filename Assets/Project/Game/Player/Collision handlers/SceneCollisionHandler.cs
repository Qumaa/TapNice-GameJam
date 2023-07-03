using UnityEngine;

namespace Project.Game
{
    public class SceneCollisionHandler : MonoBehaviour, IPlayerCollisionHandler
    {
        private IPlayerCollisionHandler _underlyingHandler;
        public void HandleCollision(PlayerCollisionInfo collision) =>
            _underlyingHandler.HandleCollision(collision);

        public void Initialize(IPlayerCollisionHandler handler) =>
            _underlyingHandler = handler;
    }
}