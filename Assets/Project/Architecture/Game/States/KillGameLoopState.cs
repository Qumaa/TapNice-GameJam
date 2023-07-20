using Project.Game.VFX;
using Project.UI;

namespace Project.Architecture.States
{
    public class KillGameLoopState : GameState
    {
        private readonly IBackgroundParticles _backgroundParticles;

        public KillGameLoopState(IGame game, IGameStateMachine stateMachine, IBackgroundParticles backgroundParticles)
            : base(game, stateMachine)
        {
            _backgroundParticles = backgroundParticles;
        }

        public override void Enter()
        {
            _game.Player.Deactivate();
            _game.LoadedLevel.OnStarted -= _backgroundParticles.Reset;
            _backgroundParticles.Reset();
            _backgroundParticles.Deactivate();

            _game.UI.Remove<IGameplayUI>();
            _game.UI.Remove<IGameplayPauseUI>();
            _game.UI.Remove<IGameplayWinUI>();

            _stateMachine.SetState<MenuState>();
        }

        public override void Exit() { }
    }
}