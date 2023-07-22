using Project.Architecture.SceneManagement;
using Project.Configs;
using Project.Game.CollisionHandling;
using Project.Game.Levels;

namespace Project.Architecture.States
{
    public readonly struct GameStateMachineDirector : IGameStateMachineDirector
    {
        private readonly IGame _game;
        private readonly ILevelDescriptor[] _levels;
        private readonly ISceneLoader _sceneLoader;
        private readonly UIConfig _uiConfig;
        private readonly ILevelUnlocker _levelUnlocker;
        private readonly INextLevelResolver _nextLevelResolver;
        private readonly ICollisionHandlerResolver _handlerResolver;
        private readonly ILevelBestTimeService _savingSystem;

        public GameStateMachineDirector(IGame game, ILevelDescriptor[] levels,
            ISceneLoader sceneLoader, UIConfig uiConfig, ILevelUnlocker levelUnlocker,
            ICollisionHandlerResolver handlerResolver, ILevelBestTimeService savingSystem)
        {
            _game = game;
            _levels = levels;
            _sceneLoader = sceneLoader;
            _uiConfig = uiConfig;
            _levelUnlocker = levelUnlocker;
            _handlerResolver = handlerResolver;
            _savingSystem = savingSystem;

            _nextLevelResolver = new NextLevelResolver(_levels.Length, _levelUnlocker);
        }

        public void Build(IGameStateMachine machine) =>
            machine.AddState(
                    new MenuState(
                        _game,
                        machine,
                        _sceneLoader,
                        _levels,
                        _uiConfig.MenuUiPrefab,
                        _levelUnlocker
                    )
                )
                .AddState(new LoadLevelState(_game, machine, _sceneLoader, _levels, _nextLevelResolver))
                .AddState(new LevelInitState(_game, machine, _handlerResolver))
                .AddState(new GameLoopState(_game, machine))
                .AddState(
                    new InitGameLoopState(
                        _game,
                        machine,
                        _uiConfig.GameUiPrefab,
                        _uiConfig.PauseUiPrefab,
                        _uiConfig.WinUiPrefab,
                        _savingSystem
                    )
                )
                .AddState(new KillGameLoopState(_game, machine, _savingSystem))
                .AddState(new PausedGameLoopState(_game, machine))
                .AddState(new RestartLevelState(_game, machine))
                .AddState(new FinishLevelState(_game, machine, _nextLevelResolver));
    }
}