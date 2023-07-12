using Project.Game;

namespace Project.Architecture
{
    public readonly struct GameStateMachineDirector : IGameStateMachineDirector
    {
        private readonly IGame _game;
        private readonly ILevelDescriptor[] _levels;
        private readonly ISceneLoader _sceneLoader;
        private readonly UIConfig _uiConfig;
        private readonly INextLevelResolver _nextLevelResolver;

        public GameStateMachineDirector(IGame game, ILevelDescriptor[] levels,
            ISceneLoader sceneLoader, UIConfig uiConfig, INextLevelResolver nextLevelResolver)
        {
            _game = game;
            _levels = levels;
            _sceneLoader = sceneLoader;
            _uiConfig = uiConfig;
            _nextLevelResolver = nextLevelResolver;
        }

        public void Build(IGameStateMachine machine) =>
            machine.AddState(new MenuState(_game, machine, _sceneLoader, _levels, _uiConfig.MenuUiPrefab))
                .AddState(new LoadLevelState(_game, machine, _sceneLoader, _levels, _nextLevelResolver))
                .AddState(new LevelInitState(_game, machine))
                .AddState(new GameLoopState(_game, machine, _nextLevelResolver))
                .AddState(new InitGameState(_game, machine, _uiConfig.GameUiPrefab))
                .AddState(new KillGameState(_game, machine));
    }
}