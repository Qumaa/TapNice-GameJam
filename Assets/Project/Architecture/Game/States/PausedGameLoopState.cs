using Project.UI;
using UnityEngine;

namespace Project.Architecture
{
    public class PausedGameLoopState : GameState
    {
        private IGameplayPauseUI _pauseUI;

        public PausedGameLoopState(IGame game, IGameStateMachine stateMachine) : base(game, stateMachine) { }
        
        public override void Enter()
        {
            _pauseUI = _game.UI.Get<IGameplayPauseUI>();
            _pauseUI.Show();

            _pauseUI.OnResumePressed += HandleResumePress;
            _pauseUI.OnRestartPressed += HandleRestartPress;
            _pauseUI.OnQuitLevelPressed += HandleQuitLevelPress;
        }

        public override void Exit()
        {
            _pauseUI.Hide();
            
            _pauseUI.OnResumePressed -= HandleResumePress;
            _pauseUI.OnRestartPressed -= HandleRestartPress;
            _pauseUI.OnQuitLevelPressed -= HandleQuitLevelPress;
        }

        private void HandleResumePress()
        {
            Debug.Log("resume pressed");
        }

        private void HandleRestartPress()
        {
            Debug.Log("restart pressed");
        }

        private void HandleQuitLevelPress()
        {
            Debug.Log("quit level pressed");
        }
    }
}