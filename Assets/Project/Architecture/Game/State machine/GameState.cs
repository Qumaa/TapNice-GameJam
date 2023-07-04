namespace Project.Architecture
{
    public abstract class GameState : IGameState
    {
        protected readonly IGame _game;
        protected readonly IGameStateMachine _stateMachine;

        protected GameState(IGame game, IGameStateMachine stateMachine)
        {
            _game = game;
            _stateMachine = stateMachine;
        }

        public abstract void Enter();

        public abstract void Exit();
    }
}
