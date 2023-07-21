using Project.Game.Levels;
using Project.Game.Player;
using Project.UI;

namespace Project.Architecture.States
{
    public class KillGameLoopState : GameState
    {
        private readonly ILevelBestTimeService _savingSystem;

        public KillGameLoopState(IGame game, IGameStateMachine stateMachine, ILevelBestTimeService savingSystem) :
            base(game, stateMachine)
        {
            _savingSystem = savingSystem;
        }

        public override void Enter()
        {
            _game.Player.Deactivate();
            _game.InputService.OnScreenTouchInput -= _game.Player.JumpIfPossible;

            _game.UI.Remove<IGameplayUI>();
            _game.UI.Remove<IGameplayPauseUI>();
            _game.UI.Remove<IGameplayWinUI>();
            
            _savingSystem.SaveLoadedData();

            _stateMachine.SetState<MenuState>();
        }

        public override void Exit() { }
    }
}