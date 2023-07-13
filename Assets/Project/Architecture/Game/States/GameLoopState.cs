using Project.Game;
using Project.UI;

namespace Project.Architecture
{
    public class GameLoopState : GameState
    {
        private readonly INextLevelResolver _levelResolver;
        private readonly UIUpdater _uiUpdater;

        public GameLoopState(IGame game, IGameStateMachine stateMachine, INextLevelResolver levelResolver) :
            base(game, stateMachine)
        {
            _levelResolver = levelResolver;
            _uiUpdater = new UIUpdater(_game.LoadedLevel);
        }

        public override void Enter()
        {
            _game.LoadedLevel.OnFinished += HandleLevelFinish;

            var ui = _game.UI.Get<IGameplayUI>();
            ui.OnPausePressed += HandlePausePress;
            _uiUpdater.Target = ui;
            _game.Add(_uiUpdater);
        }

        public override void Exit()
        {
            _game.LoadedLevel.OnFinished -= HandleLevelFinish;
            _game.UI.Get<IGameplayUI>().OnPausePressed -= HandlePausePress;
        }

        private void HandleLevelFinish()
        {
            _game.Remove(_uiUpdater);
            LoadNextLevelOrMainMenu();
        }

        private void HandlePausePress() =>
            _stateMachine.SetState<PausedGameLoopState>();

        private void LoadNextLevelOrMainMenu()
        {
            if (_levelResolver.SwitchToNextLevel(out var level))
            {
                _stateMachine.SetState<LoadLevelState, int>(level);
                return;
            }

            _stateMachine.SetState<KillGameLoopState>();
        }
        
        private class UIUpdater : IUpdatable
        {
            private readonly ILevel _source;
            public IGameplayUI Target;

            public UIUpdater(ILevel source)
            {
                _source = source;
            }

            public void Update(float timeStep) =>
                Target.DisplayTime(_source.TimeElapsed);
        }
    }
}