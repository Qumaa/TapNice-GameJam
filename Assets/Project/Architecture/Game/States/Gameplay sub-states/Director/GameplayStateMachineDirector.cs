using Project.Architecture.SceneManagement;
using Project.Game.CollisionHandling;
using Project.Game.Levels;

namespace Project.Architecture.States
{
    public readonly struct GameplayStateMachineDirector : IGameStateMachineDirector
    {
        private readonly IGame _game;
        private readonly ISceneLoader _sceneLoader;
        private readonly ILevelDescriptor[] _levels;
        private readonly ILevelResolver _levelResolver;
        private readonly ICollisionHandlerResolver _handlerResolver;
        private readonly ILevelUnlocker _levelUnlocker;
        private readonly IGameplayLeaver _gameplayLeaver;

        public GameplayStateMachineDirector(IGame game, ISceneLoader sceneLoader, ILevelDescriptor[] levels,
            ILevelResolver levelResolver, ICollisionHandlerResolver handlerResolver, ILevelUnlocker levelUnlocker, IGameplayLeaver gameplayLeaver)
        {
            _game = game;
            _sceneLoader = sceneLoader;
            _levels = levels;
            _levelResolver = levelResolver;
            _handlerResolver = handlerResolver;
            _levelUnlocker = levelUnlocker;
            _gameplayLeaver = gameplayLeaver;
        }

        public void Build(IGameStateMachine machine) =>
            machine.AddState(new LoadLevelState(_game, machine, _sceneLoader, _levels, _levelResolver))
                .AddState(new LevelInitState(_game, machine, _handlerResolver))
                .AddState(new GameLoopState(_game, machine))
                .AddState(new PausedGameLoopState(_game, machine, _gameplayLeaver))
                .AddState(new RestartLevelState(_game, machine))
                .AddState(new FinishLevelState(_game, machine, _levelResolver, _levelUnlocker, _gameplayLeaver));
    }
}