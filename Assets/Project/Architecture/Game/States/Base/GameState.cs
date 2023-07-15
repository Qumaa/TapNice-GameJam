namespace Project.Architecture.States
{
    public abstract class ExitableGameState : IExitableGameState
    {
        protected readonly IGame _game;
        protected readonly IGameStateMachine _stateMachine;

        protected ExitableGameState(IGame game, IGameStateMachine stateMachine)
        {
            _game = game;
            _stateMachine = stateMachine;
        }
        public abstract void Exit();
    }

    public abstract class GameState : ExitableGameState, IGameState
    {
        protected GameState(IGame game, IGameStateMachine stateMachine) : base(game, stateMachine)
        {
        }

        public abstract void Enter();
    }

    public abstract class GameState<T> : ExitableGameState, IGameState<T>
    {
        protected GameState(IGame game, IGameStateMachine stateMachine) : base(game, stateMachine)
        {
        }

        public abstract void Enter(T arg);
    }
}
