using Project.Configs;
using Project.Game.CollisionHandling;
using Project.Game.Effects;
using Project.Game.Player;
using Project.Game.Player.VFX;
using UnityEngine;

namespace Project.Architecture.Factories
{
    public readonly struct PlayerFactory : IFactory<IPlayer>
    {
        private readonly PlayerConfig _playerConfig;
        private readonly IEffectsManager _effectsManager;

        public PlayerFactory(PlayerConfig playerConfig, IEffectsManager effectsManager)
        {
            _playerConfig = playerConfig;
            _effectsManager = effectsManager;
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
            var colors = new PlayerColors(playerObj.GetComponent<SpriteRenderer>(), _playerConfig.PlayerCanJumpColor, 
                trail, new Affectable<Color>(_playerConfig.PlayerDefaultColor, _effectsManager));
            var rippleVFX = new ParticlePlayerRippleVFX(playerObj.GetComponent<RippleContainer>().ParticleSystem);

            var player = new PlayerScript(playerLocomotor, colors, collisionDetector, trail, rippleVFX);

            player.Deactivate();

            return player;
        }
    }
}