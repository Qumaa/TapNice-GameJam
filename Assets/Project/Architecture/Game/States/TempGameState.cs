using System;
using Project.Game;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Architecture
{
    public class TempGameState : GameState
    {
        private readonly PlayerConfig _playerConfig;
        private readonly ICollisionHandler[] _handlersTable;
        private readonly IEffectsManager _effectsManager;

        public TempGameState(IGame game, IGameStateMachine stateMachine, PlayerConfig playerConfig) : base(game,
            stateMachine)
        {
            _playerConfig = playerConfig;
            var items = Enum.GetNames(typeof(CollisionHandlerType)).Length;
            _handlersTable = new ICollisionHandler[items];
            _effectsManager = new EffectsManager();
        }

        public override void Enter()
        {
            var player = CreatePlayer();
            player.OnBounced += _effectsManager.UseEffects;
            InitCollisionHandlers();
        }

        public override void Exit()
        {
        }

        private IPlayer CreatePlayer()
        {
            var spawnPoint = Object.FindObjectOfType<PlayerSpawnPoint>();

            var obj = Object.Instantiate(_playerConfig.PlayerPrefab);

            var inputService = obj.GetComponent<IPlayerInputService>();
            var collisionDetector = obj.GetComponent<ICollisionDetector>();
            var playerLocomotor = new RigidbodyPlayerLocomotor(obj.GetComponent<Rigidbody2D>(),
                CreateAffectable(_playerConfig.JumpHeight), CreateAffectable(_playerConfig.HorizontalSpeed),
                spawnPoint.Position, spawnPoint.PlayerDirection);
            var colors = new PlayerColors(obj.GetComponent<SpriteRenderer>(), _playerConfig.PlayerDefaultColor,
                _playerConfig.PlayerCanJumpColor);

            var player = new Player(playerLocomotor);
            inputService.OnJumpInput += player.JumpIfPossible;
            player.OnCanJumpChanged += colors.UpdateColors;
            collisionDetector.OnCollided += player.HandleCollision;

            return player;
        }

        private void InitCollisionHandlers()
        {
            var handlerContainers =
                Object.FindObjectsByType<SceneCollisionHandlerContainer>(FindObjectsInactive.Include,
                    FindObjectsSortMode.None);

            if (handlerContainers.Length == 0)
                return;

            foreach (var handlerContainer in handlerContainers)
                handlerContainer.SetHandler(GetCollisionHandler(handlerContainer.HandlerType));
        }

        private ICollisionHandler GetCollisionHandler(CollisionHandlerType type) =>
            _handlersTable[(int) type] ??= CreateHandler(type);

        private static ICollisionHandler CreateHandler(CollisionHandlerType type) =>
            type switch
            {
                CollisionHandlerType.Default => new LevelCollisionHandler(),
                CollisionHandlerType.Finish => new FinishCollisionHandler(),
                CollisionHandlerType.Trampoline => new TrampolineCollisionHandler(),
                CollisionHandlerType.Discharger => new DischargerCollisionHandler(),
                _ => throw new ArgumentOutOfRangeException()
            };

        private IAffectable<float> CreateAffectable(float baseValue) =>
            new Affectable<float>(baseValue, _effectsManager);
    }
}