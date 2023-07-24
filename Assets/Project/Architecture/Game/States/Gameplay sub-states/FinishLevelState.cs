using Project.Game.Levels;
using Project.UI;

namespace Project.Architecture.States
{
    public class FinishLevelState : GameState
    {
        private readonly INextLevelResolver _levelResolver;
        private readonly ILevelUnlocker _levelUnlocker;
        private readonly IGameplayLeaver _gameplayLeaver;
        private IGameplayWinUI _winUI;

        public FinishLevelState(IGame game, IGameStateMachine stateMachine, INextLevelResolver levelResolver,
            ILevelUnlocker levelUnlocker, IGameplayLeaver gameplayLeaver) :
            base(game, stateMachine)
        {
            _levelResolver = levelResolver;
            _levelUnlocker = levelUnlocker;
            _gameplayLeaver = gameplayLeaver;
        }

        public override void Enter()
        {
            _winUI = _game.UI.Get<IGameplayWinUI>();

            _winUI.SetNextLevelButtonAvailability(_levelResolver.HasNextLevel());
            _winUI.SetElapsedTime(_game.LoadedLevel.TimeElapsed);
            _winUI.ShowAnimated();

            _winUI.OnNextLevelPressed += HandleNextLevelPress;
            _winUI.OnRestartPressed += HandleRestartPress;
            _winUI.OnQuitLevelPressed += HandleQuitLevelPress;

            _game.Pause();

            if (_levelResolver.HasNextLevel(out var nextLevel))
                _levelUnlocker.UnlockLevel(nextLevel);
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
            _gameplayLeaver.LeaveToMainMenu();

        private void HandleNextLevelPress() =>
            LoadNextLevelOrMainMenu();

        private void HandleRestartPress() =>
            _stateMachine.SetState<RestartLevelState>();

        private void HandleQuitLevelPress() =>
            LoadMainMenu();
    }
}