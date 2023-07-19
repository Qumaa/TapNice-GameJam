using DG.Tweening;
using Project.Architecture.Factories;
using Project.Architecture.SceneManagement;
using Project.Configs;
using Project.Game.CollisionHandling;
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
        private readonly ILevelDescriptor[] _levelDescriptors;
        private readonly UIConfig _uiConfig;

        public BootState(IGame game, IGameStateMachine stateMachine, PlayerConfig playerConfig,
            IEffectsManager effectsManager, UIConfig uiConfig, ISceneLoader sceneLoader, 
            ILevelDescriptor[] levelDescriptors) : 
            base(game, stateMachine)
        {
            _playerConfig = playerConfig;
            _effectsManager = effectsManager;
            _uiConfig = uiConfig;
            _sceneLoader = sceneLoader;
            _levelDescriptors = levelDescriptors;
        }

        public override void Enter()
        {
            var player = CreatePlayer();
            var level = CreateLevel(player);
            var ui = CreateUI();
            var director = CreateMachineDirector(level);

            player.OnBounced += _effectsManager.UseEffects;
            level.OnStarted += _effectsManager.ClearEffects;
            _sceneLoader.OnNewSceneLoaded += () => ui.SetCamera(Camera.main);

            _game.Player = player;
            _game.LoadedLevel = level;
            _game.UI = ui;

            director.Build(_stateMachine);

            DOTween.Init();
            Application.targetFrameRate = 60;

            MoveNext();
        }

        public override void Exit() { }

        private IPlayer CreatePlayer() =>
            new PlayerFactory(_playerConfig, _effectsManager, _game.InputService).CreateNew();

        private ILevel CreateLevel(IPlayer player) =>
            new Level(new Observable<Vector2>(Physics2D.gravity), player);

        private IGameUIRenderer CreateUI()
        {
            var obj = Object.Instantiate(_uiConfig.CanvasPrefab);

            Object.DontDestroyOnLoad(obj);

            var gameUIRenderer = new GameUIRenderer(obj.GetComponent<Canvas>());

            return gameUIRenderer;
        }

        private IGameStateMachineDirector CreateMachineDirector(ILevel level) =>
            new GameStateMachineDirector(
                _game,
                _levelDescriptors,
                _sceneLoader,
                _uiConfig,
                new NextLevelResolver(_levelDescriptors.Length),
                new CollisionHandlerResolver(level, _playerConfig)
            );

        private void MoveNext() =>
            _stateMachine.SetState<MenuState>();
    }
}