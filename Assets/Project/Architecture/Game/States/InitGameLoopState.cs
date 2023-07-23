using Project.Game.Levels;
using Project.Game.Player;
using Project.UI;
using Project.UI.Animation;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Architecture.States
{
    public class InitGameLoopState : GameState<int>
    {
        private readonly GameObject _gameplayUiPrefab;
        private readonly GameObject _pauseUiPrefab;
        private readonly GameObject _winUiPrefab;

        public InitGameLoopState(IGame game, IGameStateMachine stateMachine, GameObject gameplayUiPrefab,
            GameObject pauseUiPrefab, GameObject winUiPrefab) : base(game, stateMachine)
        {
            _gameplayUiPrefab = gameplayUiPrefab;
            _pauseUiPrefab = pauseUiPrefab;
            _winUiPrefab = winUiPrefab;
        }

        public override void Enter(int arg) =>
            LoadLevel(arg);

        private void Init()
        {
            _game.Player.Activate();
            _game.InputService.OnScreenTouchInput += _game.Player.JumpIfPossible;
            
            CreateUI();
        }

        private void LoadLevel(int level) =>
            _stateMachine.SetState<LoadLevelState, RichLoadLevelArgument>(new RichLoadLevelArgument(level, Init));

        private void CreateUI()
        {
            _game.UI.Add(HiddenUIFactory<IGameplayUI>(_gameplayUiPrefab));
            _game.UI.Add(HiddenUIFactory<IGameplayPauseUI>(_pauseUiPrefab));
            _game.UI.Add(CreateWinUI());
        }

        private IGameplayWinUI CreateWinUI()
        {
            var obj = Object.Instantiate(_winUiPrefab);

            _game.LoadedLevel.OnFinishedWithTime += obj.GetComponent<WinUIAnimator>().SetElapsedTime;

            return obj.GetComponent<IGameplayWinUI>().HideFluent();
        }

        public override void Exit() { }

        private static T UIFactory<T>(GameObject prefab)
            where T : IGameUI =>
            Object.Instantiate(prefab).GetComponent<T>();

        private static T HiddenUIFactory<T>(GameObject prefab)
            where T : IShowableGameUI =>
            UIFactory<T>(prefab).HideFluent();
    }
}