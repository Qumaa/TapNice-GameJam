using Project.Game;
using Project.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Architecture
{
    public class BootState : GameState
    {
        private readonly PlayerConfig _playerConfig;
        private readonly IEffectsManager _effectsManager;
        private readonly IGameStateMachineDirector _machineDirector;
        private readonly GameObject _uiRendererPrefab;

        public BootState(IGame game, IGameStateMachine stateMachine, PlayerConfig playerConfig,
            IEffectsManager effectsManager, IGameStateMachineDirector machineDirector, GameObject uiRendererPrefab) : base(game,
            stateMachine)
        {
            _playerConfig = playerConfig;
            _effectsManager = effectsManager;
            _machineDirector = machineDirector;
            _uiRendererPrefab = uiRendererPrefab;
        }

        public override void Enter()
        {
            var player = CreatePlayer();
            var level = CreateLevel(player);
            var ui = CreateUI();

            player.OnBounced += _effectsManager.UseEffects;
            level.OnFinished += _effectsManager.ClearEffects;

            _game.Player = player;
            _game.LoadedLevel = level;
            _game.UI = ui;

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

        private ILevel CreateLevel(IPlayer player) =>
            new Level(new Observable<Vector2>(Physics2D.gravity), player);

        private IGameUIRenderer CreateUI()
        {
            var obj = Object.Instantiate(_uiRendererPrefab);
            
            Object.DontDestroyOnLoad(obj);
            
            return new GameUIRenderer(obj.GetComponent<Canvas>());
        }

        private void MoveNext() =>
            _stateMachine.SetState<MenuState>();

        private IAffectable<T> CreateAffectable<T>(T baseValue) =>
            new Affectable<T>(baseValue, _effectsManager);
    }
}