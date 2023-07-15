namespace Project.Architecture.States
{
    public interface IGameStateMachineDirector
    {
        void Build(IGameStateMachine machine);
    }
}