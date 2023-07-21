using Project.Game.Player;
using Project.UI;

namespace Project.Architecture.States
{
    public class KillGameLoopState : GameState
    {

        public KillGameLoopState(IGame game, IGameStateMachine stateMachine)
            : base(game, stateMachine)
        {
        }

        public override void Enter()
        {
            _game.Player.Deactivate();
            _game.InputService.OnScreenTouchInput -= _game.Player.JumpIfPossible;

            _game.UI.Remove<IGameplayUI>();
            _game.UI.Remove<IGameplayPauseUI>();
            _game.UI.Remove<IGameplayWinUI>();

            _stateMachine.SetState<MenuState>();
        }

        public override void Exit() { }
    }
}