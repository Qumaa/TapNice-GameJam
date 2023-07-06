namespace Project.Architecture
{
    public interface IGameState : IExitableGameState, IEnterableGameState
    {
    }

    public interface IGameState<TArgument> : IExitableGameState, IEnterableGameState<TArgument>
    {
    }

    public interface IEnterableGameState
    {
        void Enter();
    }

    public interface IEnterableGameState<TArgument>
    {
        void Enter(TArgument arg);
    }

    public interface IExitableGameState
    {
        void Exit();
    }
}