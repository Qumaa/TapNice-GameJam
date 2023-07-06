namespace Project.Architecture
{
    public class LoadLevelState : ExitableGameState, IEnterableGameState<string>, IEnterableGameState<ushort>
    {
        public LoadLevelState(IGame game, IGameStateMachine stateMachine) : base(game, stateMachine)
        {
        }

        public void Enter(string sceneName)
        {
            //todo: scene loading
        }

        public void Enter(ushort arg)
        {
        }

        public override void Exit()
        {
        }
    }
}