using Project.UI;
using UnityEngine;

namespace Project.Architecture
{
    public class InitGameLoopState : GameState<int>
    {
        private readonly GameObject _gameplayUiPrefab;
        private readonly GameObject _pauseUiPrefab;

        public InitGameLoopState(IGame game, IGameStateMachine stateMachine, GameObject gameplayUiPrefab,
            GameObject pauseUiPrefab) : base(game, stateMachine)
        {
            _gameplayUiPrefab = gameplayUiPrefab;
            _pauseUiPrefab = pauseUiPrefab;
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
            _game.UI.Add(UIFactory<IGameplayUI>(_gameplayUiPrefab));
            _game.UI.Add(UIFactory<IGameplayPauseUI>(_pauseUiPrefab));
        }

        public override void Exit() { }

        private static T UIFactory<T>(GameObject prefab) where T : IGameUI =>
            Object.Instantiate(prefab).GetComponent<T>();
    }
}