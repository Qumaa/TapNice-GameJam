using Project.Game;

namespace Project.Architecture
{
    public readonly struct GameStateMachineDirector : IGameStateMachineDirector
    {
        private readonly IGame _game;
        private readonly ILevelDescriptor[] _levels;
        private readonly ISceneLoader _sceneLoader;

        public GameStateMachineDirector(IGame game, ILevelDescriptor[] levels,
            ISceneLoader sceneLoader)
        {
            _game = game;
            _levels = levels;
            _sceneLoader = sceneLoader;
        }

        public void Build(IGameStateMachine machine) =>
            machine.AddState(new MenuState(_game, machine, _sceneLoader, _levels))
                .AddState(new LoadLevelState(_game, machine, _sceneLoader))
                .AddState(new LevelInitState(_game, machine))
                .AddState(new GameLoopState(_game, machine));
    }
}