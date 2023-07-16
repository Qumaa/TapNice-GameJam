using Project.Game.Levels;
using Project.UI;

namespace Project.Architecture.States
{
    // todo: display elapsed time
    public class FinishLevelState : GameState
    {
        private readonly INextLevelResolver _levelResolver;
        private IGameplayWinUI _winUI;

        public FinishLevelState(IGame game, IGameStateMachine stateMachine, INextLevelResolver levelResolver) : 
            base(game, stateMachine)
        {
            _levelResolver = levelResolver;
        }

        public override void Enter()
        {
            _winUI = _game.UI.Get<IGameplayWinUI>();
            
            _winUI.SetNextLevelButtonAvailability(_levelResolver.HasNextLevel());
            _winUI.SetElapsedTime(_game.LoadedLevel.TimeElapsed);
            _winUI.Show();

            _winUI.OnNextLevelPressed += HandleNextLevelPress;
            _winUI.OnRestartPressed += HandleRestartPress;
            _winUI.OnQuitLevelPressed += HandleQuitLevelPress;
            
            _game.Pause();
        }

        public override void Exit()
        {
            _winUI.Hide();
            
            _winUI.OnNextLevelPressed -= HandleNextLevelPress;
            _winUI.OnRestartPressed -= HandleRestartPress;
            _winUI.OnQuitLevelPressed -= HandleQuitLevelPress;
            
            _game.Resume();
        }

        private void LoadNextLevelOrMainMenu()
        {
            if (TryLoadNextLevel())
                return;

            LoadMainMenu();
        }

        private bool TryLoadNextLevel()
        {
            if (!_levelResolver.TrySwitchToNextLevel(out var level))
                return false;
            
            _stateMachine.SetState<LoadLevelState, int>(level);
            return true;

        }

        private void LoadMainMenu() =>
            _stateMachine.SetState<KillGameLoopState>();

        private void HandleNextLevelPress() =>
            LoadNextLevelOrMainMenu();

        private void HandleRestartPress() =>
            _stateMachine.SetState<RestartLevelState>();

        private void HandleQuitLevelPress() =>
            LoadMainMenu();
    }
}