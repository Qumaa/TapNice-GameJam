namespace Project.Architecture
{
    public class GameLoopState : GameState
    {
        public GameLoopState(IGame game, IGameStateMachine stateMachine) : base(game, stateMachine)
        {
        }

        public override void Enter()
        {
            _game.LoadedLevel.OnFinishedWithTime += HandleLevelFinish;
        }

        private void HandleLevelFinish(float time)
        {
            LoadNextLevelOrMainMenu();
        }

        public override void Exit()
        {
            _game.LoadedLevel.OnFinishedWithTime -= HandleLevelFinish;
        }

        private void LoadNextLevelOrMainMenu()
        {
            if (!_game.LoadNextLevel())
                _game.LoadMainMenu();
        }
    }
}