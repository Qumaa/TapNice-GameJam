using Project.Game.Player;
using Project.UI;

namespace Project.Architecture.States
{
    public class KillGameLoopState : GameState
    {
        private readonly IPersistentDataProcessor[] _dataProcessors;

        public KillGameLoopState(IGame game, IGameStateMachine stateMachine,
            params IPersistentDataProcessor[] dataProcessors) :
            base(game, stateMachine)
        {
            _dataProcessors = dataProcessors;
        }

        public override void Enter()
        {
            _game.Player.Deactivate();
            _game.InputService.OnScreenTouchInput -= _game.Player.JumpIfPossible;

            _game.UI.Remove<IGameplayUI>();
            _game.UI.Remove<IGameplayPauseUI>();
            _game.UI.Remove<IGameplayWinUI>();

            foreach (var processor in _dataProcessors)
                processor.SaveLoadedData();

            _stateMachine.SetState<MenuState>();
        }

        public override void Exit() { }
    }
}