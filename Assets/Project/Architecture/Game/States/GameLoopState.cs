namespace Project.Architecture
{
    public class GameLoopState : GameState
    {
        private readonly INextLevelResolver _levelResolver;

        public GameLoopState(IGame game, IGameStateMachine stateMachine, INextLevelResolver levelResolver) :
            base(game, stateMachine)
        {
            _levelResolver = levelResolver;
        }

        public override void Enter()
        {
            _game.LoadedLevel.OnFinished += HandleLevelFinish;
        }

        public override void Exit()
        {
            _game.LoadedLevel.OnFinished -= HandleLevelFinish;
        }

        private void HandleLevelFinish()
        {
            LoadNextLevelOrMainMenu();
        }

        private void LoadNextLevelOrMainMenu()
        {
            if (_levelResolver.SwitchToNextLevel(out var level))
            {
                _stateMachine.SetState<LoadLevelState, int>(level);
                return;
            }

            _stateMachine.SetState<KillGameState>();
        }
    }
}