using Project.UI;

namespace Project.Architecture.States
{
    public class PausedGameLoopState : GameState
    {
        private IGameplayPauseUI _pauseUI;

        public PausedGameLoopState(IGame game, IGameStateMachine stateMachine) : base(game, stateMachine) { }
        
        public override void Enter()
        {
            _pauseUI = _game.UI.Get<IGameplayPauseUI>();
            _pauseUI.Show();

            _pauseUI.OnResumePressed += HandleResumePress;
            _pauseUI.OnRestartPressed += HandleRestartPress;
            _pauseUI.OnQuitLevelPressed += HandleQuitLevelPress;
            
            _game.Pause();
        }

        public override void Exit()
        {
            _pauseUI.Hide();
            
            _pauseUI.OnResumePressed -= HandleResumePress;
            _pauseUI.OnRestartPressed -= HandleRestartPress;
            _pauseUI.OnQuitLevelPressed -= HandleQuitLevelPress;
            
            _game.Resume();
        }

        private void HandleResumePress() =>
            _stateMachine.SetState<GameLoopState>();

        private void HandleRestartPress() =>
            _stateMachine.SetState<RestartLevelState>();

        private void HandleQuitLevelPress() =>
            _stateMachine.SetState<KillGameLoopState>();
    }
}