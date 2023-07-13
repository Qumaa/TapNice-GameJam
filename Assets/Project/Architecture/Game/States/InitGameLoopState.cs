using Project.UI;
using UnityEngine;

namespace Project.Architecture
{
    public class InitGameLoopState : GameState<int>
    {
        private readonly GameObject _uiPrefab;

        public InitGameLoopState(IGame game, IGameStateMachine stateMachine, GameObject uiPrefab) : base(game, stateMachine)
        {
            _uiPrefab = uiPrefab;
        }

        public override void Enter(int arg)
        {
            CreateUI();
            LoadLevel(arg);
        }

        private void LoadLevel(int level) =>
            _stateMachine.SetState<LoadLevelState, int>(level);

        private void CreateUI()
        {
            var gameplayUI = Object.Instantiate(_uiPrefab).GetComponent<IGameplayUI>();
            _game.UI.Add(gameplayUI);
        }

        public override void Exit() { }
    }
}