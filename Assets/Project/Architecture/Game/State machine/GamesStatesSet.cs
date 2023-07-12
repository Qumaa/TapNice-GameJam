namespace Project.Architecture
{
    public abstract class GamesStatesSet : GameStateMachine, IGameState
    {
        public abstract void Exit();

        public abstract void Enter();
    }
    
    public abstract class GamesStatesSet<T> : GameStateMachine, IGameState<T>
    {
        public abstract void Exit();

        public abstract void Enter(T arg);
    }
}