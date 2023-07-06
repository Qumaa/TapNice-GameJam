using Project.Game;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Architecture
{
    public class BootState : GameState
    {
        private readonly PlayerConfig _playerConfig;
        private readonly IEffectsManager _effectsManager;

        public BootState(IGame game, IGameStateMachine stateMachine, PlayerConfig playerConfig) : base(game,
            stateMachine)
        {
            _playerConfig = playerConfig;
            _effectsManager = new EffectsManager();
        }

        public override void Enter()
        {
            var player = CreatePlayer();
            player.OnBounced += _effectsManager.UseEffects;
        }

        public override void Exit()
        {
        }

        private IPlayer CreatePlayer()
        {
            var spawnPoint = Object.FindObjectOfType<PlayerSpawnPoint>();

            var playerObj = Object.Instantiate(_playerConfig.PlayerPrefab);
            Object.DontDestroyOnLoad(playerObj);

            var inputService = playerObj.GetComponent<IPlayerInputService>();
            var collisionDetector = playerObj.GetComponent<ICollisionDetector>();
            var playerLocomotor = new RigidbodyPlayerLocomotor(playerObj.GetComponent<Rigidbody2D>(),
                CreateAffectable(_playerConfig.JumpHeight), CreateAffectable(_playerConfig.HorizontalSpeed),
                spawnPoint.Position, spawnPoint.PlayerDirection);
            var colors = new PlayerColors(playerObj.GetComponent<SpriteRenderer>(), _playerConfig.PlayerDefaultColor,
                _playerConfig.PlayerCanJumpColor);

            var player = new Player(playerLocomotor);
            inputService.OnJumpInput += player.JumpIfPossible;
            player.OnCanJumpChanged += colors.UpdateColors;
            collisionDetector.OnCollided += player.HandleCollision;
            player.Reset();

            return player;
        }

        private IAffectable<float> CreateAffectable(float baseValue) =>
            new Affectable<float>(baseValue, _effectsManager);
    }
}