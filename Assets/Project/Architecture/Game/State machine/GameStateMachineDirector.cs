namespace Project.Architecture
{
    public struct GameStateMachineDirector : IGameStateMachineDirector
    {
        private IGame _game;
        private PlayerConfig _playerConfig;

        public GameStateMachineDirector(IGame game, PlayerConfig playerConfig)
        {
            _game = game;
            _playerConfig = playerConfig;
        }

        public void Build(IGameStateMachine machine)
        {
            machine.AddState(new TempGameState(_game, machine, _playerConfig));
        }
    }
}