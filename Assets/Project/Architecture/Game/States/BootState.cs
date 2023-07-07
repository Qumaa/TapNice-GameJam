using Project.Game;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Architecture
{
    public class BootState : GameState
    {
        private readonly PlayerConfig _playerConfig;
        private readonly IEffectsManager _effectsManager;

        public BootState(IGame game, IGameStateMachine stateMachine, PlayerConfig playerConfig, IEffectsManager effectsManager) : base(game,
            stateMachine)
        {
            _playerConfig = playerConfig;
            _effectsManager = effectsManager;
        }

        public override void Enter()
        {
            var player = CreatePlayer();
            player.OnBounced += _effectsManager.UseEffects;

            _game.Player = player;
            
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
            
            player.Deactivate();

            return player;
        }

        private void MoveNext() =>
            _stateMachine.SetState<MenuState>();

        private IAffectable<float> CreateAffectable(float baseValue) =>
            new Affectable<float>(baseValue, _effectsManager);
    }
}