using Project.Configs;
using Project.Game.Player;
using Project.UI;
using Project.UI.Animation;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Architecture.States
{
    internal class GameplayInitializer
    {
        private readonly IGame _game;
        private readonly IGameStateMachine _stateMachine;
        private readonly GameObject _gameplayUiPrefab;
        private readonly GameObject _pauseUiPrefab;
        private readonly GameObject _winUiPrefab;
        private readonly IPersistentDataProcessor[] _dataProcessors;

        private WinUIAnimator _winUIAnimator;

        public GameplayInitializer(IGame game, IGameStateMachine stateMachine, UIConfig uiConfig,
            params IPersistentDataProcessor[] dataProcessors)
        {
            _game = game;
            _stateMachine = stateMachine;
            _gameplayUiPrefab = uiConfig.GameUiPrefab;
            _pauseUiPrefab = uiConfig.PauseUiPrefab;
            _winUiPrefab = uiConfig.WinUiPrefab;
            _dataProcessors = dataProcessors;
        }

        public void InitializeGameplay(int levelToLoad) =>
            _stateMachine.SetState<LoadLevelState, RichLoadLevelArgument>(new RichLoadLevelArgument(levelToLoad, Init));

        public void KillGameplay()
        {
            _game.Player.Deactivate();
            _game.InputService.OnScreenTouchInput -= _game.Player.JumpIfPossible;

            _game.UI.Remove<IGameplayUI>();
            _game.UI.Remove<IGameplayPauseUI>();
            _game.UI.Remove<IGameplayWinUI>();

            _game.LoadedLevel.OnFinishedWithTime -= _winUIAnimator.SetElapsedTime;

            foreach (var processor in _dataProcessors)
                processor.SaveLoadedData();
        }

        private void Init()
        {
            _game.Player.Activate();
            _game.InputService.OnScreenTouchInput += _game.Player.JumpIfPossible;

            _game.UI.Add(HiddenUIFactory<IGameplayUI>(_gameplayUiPrefab));
            _game.UI.Add(HiddenUIFactory<IGameplayPauseUI>(_pauseUiPrefab));
            _game.UI.Add(CreateWinUI());
        }

        private IGameplayWinUI CreateWinUI()
        {
            var obj = Object.Instantiate(_winUiPrefab);

            _winUIAnimator = obj.GetComponent<WinUIAnimator>();
            _game.LoadedLevel.OnFinishedWithTime += _winUIAnimator.SetElapsedTime;

            return obj.GetComponent<IGameplayWinUI>().HideFluent();
        }

        private static T UIFactory<T>(GameObject prefab)
            where T : IGameUI =>
            Object.Instantiate(prefab).GetComponent<T>();

        private static T HiddenUIFactory<T>(GameObject prefab)
            where T : IShowableGameUI =>
            UIFactory<T>(prefab).HideFluent();
    }
}