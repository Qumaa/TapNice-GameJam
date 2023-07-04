namespace Project.Architecture
{
    public interface IGameStateMachine
    {
        IGameStateMachine AddState<T>(T state)
            where T : IGameState;

        void SetState<T>()
            where T : IGameState;
    }
}