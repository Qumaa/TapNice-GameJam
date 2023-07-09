using Project.Game;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Architecture
{
    public class BootState : GameState
    {
        private readonly PlayerConfig _playerConfig;
        private readonly IEffectsManager _effectsManager;
        private readonly IGameStateMachineDirector _machineDirector;

        public BootState(IGame game, IGameStateMachine stateMachine, PlayerConfig playerConfig,
            IEffectsManager effectsManager, IGameStateMachineDirector machineDirector) : base(game,
            stateMachine)
        {
            _playerConfig = playerConfig;
            _effectsManager = effectsManager;
            _machineDirector = machineDirector;
        }

        public override void Enter()
        {
            var player = CreatePlayer();
            player.OnBounced += _effectsManager.UseEffects;

            _game.Player = player;

            _machineDirector.Build(_stateMachine);

            MoveNext();
        }

        public override void Exit()
        {
        }

        private IPlayer CreatePlayer()
        {
            var playerObj = Object.Instantiate(_playerConfig.PlayerPrefab);
            Object.DontDestroyOnLoad(playerObj);

            var collisionDetector = playerObj.GetComponent<ICollisionDetector>();
            var playerLocomotor = new RigidbodyPlayerLocomotor(playerObj.GetComponent<Rigidbody2D>(),
                CreateAffectable(_playerConfig.JumpHeight), CreateAffectable(_playerConfig.HorizontalSpeed));
            var colors = new PlayerColors(playerObj.GetComponent<SpriteRenderer>(), _playerConfig.PlayerDefaultColor,
                _playerConfig.PlayerCanJumpColor);

            var player = new Player(playerLocomotor, colors, _game.InputService, collisionDetector);

            return player;
        }

        private void MoveNext() =>
            _stateMachine.SetState<MenuState>();

        private IAffectable<T> CreateAffectable<T>(T baseValue) =>
            new Affectable<T>(baseValue, _effectsManager);
    }
}