namespace Project.Architecture
{
    public struct GameStateMachineDirector : IGameStateMachineDirector
    {
        private readonly IGame _game;
        private readonly PlayerConfig _playerConfig;

        public GameStateMachineDirector(IGame game, PlayerConfig playerConfig)
        {
            _game = game;
            _playerConfig = playerConfig;
        }

        public void Build(IGameStateMachine machine)
        {
            machine.AddState(new BootState(_game, machine, _playerConfig))
                .AddState(new LoadLevelState(_game, machine))
                .AddState(new LevelInitState(_game, machine));
        }
    }
}