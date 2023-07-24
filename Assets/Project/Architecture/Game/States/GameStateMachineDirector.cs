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
        private readonly ILevelBestTimeService _levelBestTimeService;

        public GameStateMachineDirector(IGame game, ILevelDescriptor[] levels,
            ISceneLoader sceneLoader, UIConfig uiConfig, ILevelUnlocker levelUnlocker,
            INextLevelResolver nextLevelResolver,
            ICollisionHandlerResolver handlerResolver,
            ILevelBestTimeService levelBestTimeService)
        {
            _game = game;
            _levels = levels;
            _sceneLoader = sceneLoader;
            _uiConfig = uiConfig;
            _levelUnlocker = levelUnlocker;
            _handlerResolver = handlerResolver;
            _nextLevelResolver = nextLevelResolver;
            _levelBestTimeService = levelBestTimeService;
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
                .AddState(
                    new GameplayState(
                        _game,
                        machine,
                        _sceneLoader,
                        _levels,
                        _nextLevelResolver,
                        _handlerResolver,
                        _uiConfig,
                        _levelBestTimeService,
                        _levelUnlocker
                    )
                );
    }
}