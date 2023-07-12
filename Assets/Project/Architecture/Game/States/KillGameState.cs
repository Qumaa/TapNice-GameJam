using Project.UI;

namespace Project.Architecture
{
    public class KillGameState : GameState
    {
        public KillGameState(IGame game, IGameStateMachine stateMachine) : base(game, stateMachine) { }

        public override void Enter()
        {
            _game.UI.Remove<IGameplayUI>();
            _stateMachine.SetState<MenuState>();
        }

        public override void Exit() { }
    }
}