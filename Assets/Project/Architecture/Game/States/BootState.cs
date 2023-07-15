using Project.Architecture.Factories;
using Project.Architecture.SceneManagement;
using Project.Configs;
using Project.Game.Effects;
using Project.Game.Levels;
using Project.Game.Player;
using Project.UI;
using UnityEngine;

namespace Project.Architecture.States
{
    public class BootState : GameState
    {
        private readonly IEffectsManager _effectsManager;
        private readonly PlayerConfig _playerConfig;
        private readonly ISceneLoader _sceneLoader;
        private readonly GameObject _uiRendererPrefab;
        private IGameStateMachineDirector _machineDirector;

        public BootState(IGame game, IGameStateMachine stateMachine, PlayerConfig playerConfig,
            IEffectsManager effectsManager, IGameStateMachineDirector machineDirector,
            GameObject uiRendererPrefab, ISceneLoader sceneLoader) : base(game,
            stateMachine)
        {
            _playerConfig = playerConfig;
            _effectsManager = effectsManager;
            _machineDirector = machineDirector;
            _uiRendererPrefab = uiRendererPrefab;
            _sceneLoader = sceneLoader;
        }

        public override void Enter()
        {
            var player = CreatePlayer();
            var level = CreateLevel(player);
            var ui = CreateUI();

            player.OnBounced += _effectsManager.UseEffects;
            level.OnFinished += _effectsManager.ClearEffects;
            _sceneLoader.OnNewSceneLoaded += () => ui.SetCamera(Camera.main);

            _game.Player = player;
            _game.LoadedLevel = level;
            _game.UI = ui;

            _machineDirector.Build(_stateMachine);
            _machineDirector = null;

            MoveNext();
        }

        public override void Exit() { }

        private IPlayer CreatePlayer() =>
            new PlayerFactory(_playerConfig, _effectsManager, _game.InputService).CreateNew();

        private ILevel CreateLevel(IPlayer player) =>
            new Level(new Observable<Vector2>(Physics2D.gravity), player);

        private IGameUIRenderer CreateUI()
        {
            var obj = Object.Instantiate(_uiRendererPrefab);

            Object.DontDestroyOnLoad(obj);

            var gameUIRenderer = new GameUIRenderer(obj.GetComponent<Canvas>());

            return gameUIRenderer;
        }

        private void MoveNext() =>
            _stateMachine.SetState<MenuState>();
    }
}