namespace Project.Architecture.States
{
    public class LevelInitState : GameState
    {
        
        public LevelInitState(IGame game, IGameStateMachine stateMachine) : base(game, stateMachine)
        {
            
        }

        public override void Enter()
        {
            _game.LoadedLevel.Start();
            MoveNext();
        }

        public override void Exit()
        {
        }

        private void MoveNext() =>
            _stateMachine.SetState<GameLoopState>();
    }
}