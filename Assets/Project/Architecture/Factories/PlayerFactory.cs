using Project.Configs;
using Project.Game.CollisionHandling;
using Project.Game.Effects;
using Project.Game.Player;
using UnityEngine;

namespace Project.Architecture.Factories
{
    public readonly struct PlayerFactory : IFactory<IPlayer>
    {
        private readonly PlayerConfig _playerConfig;
        private readonly IEffectsManager _effectsManager;
        private readonly IGameInputService _inputService;

        public PlayerFactory(PlayerConfig playerConfig, IEffectsManager effectsManager, IGameInputService inputService)
        {
            _playerConfig = playerConfig;
            _effectsManager = effectsManager;
            _inputService = inputService;
        }

        public IPlayer CreateNew()
        {
            var playerObj = Object.Instantiate(_playerConfig.PlayerPrefab);
            Object.DontDestroyOnLoad(playerObj);
            var trailsTransform = playerObj.GetComponentInChildren<PlayerTrailTransformContainer>().TrailsTransform;

            var trailFactory =
                new PlayerTrailRendererFactory(_playerConfig.TrailPrefab, trailsTransform, _playerConfig.TrailTime);
            var trail = new PlayerTrail(new Container<TrailRendererWrapper>(trailFactory));
            var collisionDetector = playerObj.GetComponent<ICollisionDetector>();
            var playerLocomotor = new RigidbodyPlayerLocomotor(playerObj.GetComponent<Rigidbody2D>(),
                new Affectable<float>(_playerConfig.JumpHeight, _effectsManager),
                new Affectable<float>(_playerConfig.HorizontalSpeed, _effectsManager));
            var colors = new PlayerColors(playerObj.GetComponent<SpriteRenderer>(), _playerConfig.PlayerDefaultColor,
                _playerConfig.PlayerCanJumpColor, trail);

            var player = new PlayerScript(playerLocomotor, colors, _inputService, collisionDetector, trail);

            return player;
        }
    }
}