using System;
using Project.Game;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Architecture
{
    public class TempGameState : GameState
    {
        private readonly PlayerConfig _playerConfig;
        private readonly ICollisionHandler[] _handlersTable;
        private readonly IEffectsManager _effectsManager;

        public TempGameState(IGame game, IGameStateMachine stateMachine, PlayerConfig playerConfig) : base(game, stateMachine)
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

        private void InitCollisionHandlers()
        {
            var handlerContainers =
                Object.FindObjectsByType<SceneCollisionHandlerContainer>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            
            if (handlerContainers.Length == 0)
                return;
            
            foreach(var handlerContainer in handlerContainers)
                handlerContainer.SetHandler(GetCollisionHandler(handlerContainer.HandlerType));
        }

        private IPlayer CreatePlayer()
        {
            var obj = Object.Instantiate(_playerConfig.PlayerPrefab);
            var player = obj.GetComponent<IPlayer>();
            
            player.HorizontalSpeed = _playerConfig.HorizontalSpeed;
            player.JumpHeight = _playerConfig.JumpHeight;
            player.SetColors(_playerConfig.PlayerDefaultColor, _playerConfig.PlayerCanJumpColor);

            return player;
        }

        public override void Exit()
        {
        }

        private ICollisionHandler GetCollisionHandler(CollisionHandlerType type) =>
            _handlersTable[(int) type] ??= CreateHandler(type);

        private ICollisionHandler CreateHandler(CollisionHandlerType type) =>
            type switch
            {
                CollisionHandlerType.Default => new LevelCollisionHandler(),
                CollisionHandlerType.Finish => new FinishCollisionHandler(_effectsManager),
                CollisionHandlerType.Trampoline => throw new NotImplementedException(),
                CollisionHandlerType.Discharger => throw new NotImplementedException(),
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}