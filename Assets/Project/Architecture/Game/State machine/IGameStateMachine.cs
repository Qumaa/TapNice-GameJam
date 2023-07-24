namespace Project.Architecture.States
{
    public interface IGameStateMachine
    {
        void AddSingleState<T>(T state) where T : IExitableGameState;
        void SetState<T>() where T : IEnterableGameState;
        void SetState<T, TArg>(TArg arg) where T : IEnterableGameState<TArg>;
        void ExitCurrentState();
    }

    public static class GameStateMachineExtensions
    {
        public static IGameStateMachine AddState<T>(this IGameStateMachine machine, T state) where T : IExitableGameState
        {
            machine.AddSingleState(state);
            return machine;
        }
    }
}