namespace Project.Architecture
{
    public interface IGameStateMachine
    {
        IGameStateMachine AddState<T>(T state)
            where T : IExitableGameState;

        void SetState<T>()
            where T : IEnterableGameState;

        void SetState<T, TArg>(TArg arg)
            where T : IEnterableGameState<TArg>;
    }
}