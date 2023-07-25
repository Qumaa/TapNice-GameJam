using Project.Game.Levels;
using Project.UI;
using Project.UI.Animation;

namespace Project.Architecture.States
{
    public class GameLoopState : GameState
    {
        private readonly UIUpdater _uiUpdater;

        public GameLoopState(IGame game, IGameStateMachine stateMachine) :
            base(game, stateMachine)
        {
            _uiUpdater = new UIUpdater(_game.LoadedLevel);
        }

        public override void Enter()
        {
            _game.LoadedLevel.OnFinished += HandleLevelFinish;

            var ui = _game.UI.Get<IGameplayUI>().ShowAnimatedFluent();
            ui.OnPausePressed += HandlePausePress;
            ui.SetLevelName(_game.LoadedLevel.Index + 1, _game.LoadedLevel.Name);
            SetBestTime(ui);
            
            _uiUpdater.Target = ui;
            _game.Add(_uiUpdater);
        }

        public override void Exit()
        {
            _game.Remove(_uiUpdater);
            _game.LoadedLevel.OnFinished -= HandleLevelFinish;
            
            var ui = _game.UI.Get<IGameplayUI>().HideAnimatedFluent();
            ui.OnPausePressed -= HandlePausePress;
        }

        private void HandleLevelFinish() =>
            _stateMachine.SetState<FinishLevelState>();

        private void HandlePausePress() =>
            _stateMachine.SetState<PausedGameLoopState>();

        private void SetBestTime(IGameplayUI ui)
        {
            if (_game.LoadedLevel.BestTime.IsEmpty)
                ui.HideBestTime();
            else
                ui.SetBestTime(_game.LoadedLevel.BestTime.AsSeconds);
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