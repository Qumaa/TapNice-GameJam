using Project.UI;

namespace Project.Architecture
{
    public class KillGameLoopState : GameState
    {
        public KillGameLoopState(IGame game, IGameStateMachine stateMachine) : base(game, stateMachine) { }

        public override void Enter()
        {
            _game.UI.Remove<IGameplayUI>();
            _game.UI.Remove<IGameplayPauseUI>();
            _stateMachine.SetState<MenuState>();
        }

        public override void Exit() { }
    }
}