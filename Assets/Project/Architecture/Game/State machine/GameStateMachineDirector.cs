using Project.Architecture.SceneManagement;
using Project.Configs;
using Project.Game.CollisionHandling;
using Project.Game.Levels;
using Project.Game.VFX;

namespace Project.Architecture.States
{
    public readonly struct GameStateMachineDirector : IGameStateMachineDirector
    {
        private readonly IGame _game;
        private readonly ILevelDescriptor[] _levels;
        private readonly ISceneLoader _sceneLoader;
        private readonly UIConfig _uiConfig;
        private readonly INextLevelResolver _nextLevelResolver;
        private readonly ICollisionHandlerResolver _handlerResolver;
        private readonly IBackgroundParticles _backgroundParticles;

        public GameStateMachineDirector(IGame game, ILevelDescriptor[] levels,
            ISceneLoader sceneLoader, UIConfig uiConfig, INextLevelResolver nextLevelResolver,
            ICollisionHandlerResolver handlerResolver, IBackgroundParticles backgroundParticles)
        {
            _game = game;
            _levels = levels;
            _sceneLoader = sceneLoader;
            _uiConfig = uiConfig;
            _nextLevelResolver = nextLevelResolver;
            _handlerResolver = handlerResolver;
            _backgroundParticles = backgroundParticles;
        }

        public void Build(IGameStateMachine machine) =>
            machine.AddState(new MenuState(_game, machine, _sceneLoader, _levels, _uiConfig.MenuUiPrefab))
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
                        _backgroundParticles
                    )
                )
                .AddState(new KillGameLoopState(_game, machine, _backgroundParticles))
                .AddState(new PausedGameLoopState(_game, machine))
                .AddState(new RestartLevelState(_game, machine))
                .AddState(new FinishLevelState(_game, machine, _nextLevelResolver));
    }
}