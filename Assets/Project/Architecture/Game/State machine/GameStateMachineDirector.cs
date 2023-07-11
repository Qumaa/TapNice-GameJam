using Project.Game;

namespace Project.Architecture
{
    public readonly struct GameStateMachineDirector : IGameStateMachineDirector
    {
        private readonly IGame _game;
        private readonly ILevelDescriptor[] _levels;
        private readonly ISceneLoader _sceneLoader;
        private readonly UIConfig _uiConfig;

        public GameStateMachineDirector(IGame game, ILevelDescriptor[] levels,
            ISceneLoader sceneLoader, UIConfig uiConfig)
        {
            _game = game;
            _levels = levels;
            _sceneLoader = sceneLoader;
            _uiConfig = uiConfig;
        }

        public void Build(IGameStateMachine machine) =>
            machine.AddState(new MenuState(_game, machine, _sceneLoader, _levels, _uiConfig.MenuUiPrefab, _uiConfig.GameUiPrefab))
                .AddState(new LoadLevelState(_game, machine, _sceneLoader, _levels))
                .AddState(new LevelInitState(_game, machine))
                .AddState(new GameLoopState(_game, machine));
    }
}