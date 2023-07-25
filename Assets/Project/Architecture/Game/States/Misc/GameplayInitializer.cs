using Project.Configs;
using Project.Game.Levels;
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
        private readonly ILevelDescriptor[] _levelDescriptors;
        private readonly ILevelBestTimeService _levelBestTimeService;
        private readonly GameObject _gameplayUiPrefab;
        private readonly GameObject _pauseUiPrefab;
        private readonly GameObject _winUiPrefab;
        private readonly IPersistentDataProcessor[] _dataProcessors;

        private WinUIAnimator _winUIAnimator;
        private int _loadedLevel;

        public GameplayInitializer(IGame game, IGameStateMachine stateMachine, UIConfig uiConfig,
            ILevelDescriptor[] levelDescriptors, ILevelBestTimeService levelBestTimeService,
            params IPersistentDataProcessor[] dataProcessors)
        {
            _game = game;
            _stateMachine = stateMachine;
            _levelDescriptors = levelDescriptors;
            _levelBestTimeService = levelBestTimeService;
            _gameplayUiPrefab = uiConfig.GameUiPrefab;
            _pauseUiPrefab = uiConfig.PauseUiPrefab;
            _winUiPrefab = uiConfig.WinUiPrefab;
            _dataProcessors = dataProcessors;
        }

        public void InitializeGameplay(int levelToLoad)
        {
            _loadedLevel = levelToLoad;
            _stateMachine.SetState<LoadLevelState, RichLoadLevelArgument>(new RichLoadLevelArgument(levelToLoad, Init));
        }

        private void Init()
        {
            _game.Player.Activate();
            _game.InputService.OnScreenTouchInput += _game.Player.JumpIfPossible;
            _game.LoadedLevel.OnFinishedWithTime += UpdateBestTime;

            var ui = HiddenUIFactory<IGameplayUI>(_gameplayUiPrefab);
            _game.UI.Add(ui);
            _game.UI.Add(HiddenUIFactory<IGameplayPauseUI>(_pauseUiPrefab));
            _game.UI.Add(CreateWinUI());

            var levelName = _levelDescriptors[_loadedLevel].LevelName;
            ui.SetLevelName(_loadedLevel + 1, levelName);
            SetBestTime(ui, levelName);
        }

        public void KillGameplay()
        {
            _game.Player.Deactivate();
            _game.InputService.OnScreenTouchInput -= _game.Player.JumpIfPossible;
            _game.LoadedLevel.OnFinishedWithTime -= UpdateBestTime;

            _game.UI.Remove<IGameplayUI>();
            _game.UI.Remove<IGameplayPauseUI>();
            _game.UI.Remove<IGameplayWinUI>();

            _game.LoadedLevel.OnFinishedWithTime -= _winUIAnimator.SetElapsedTime;

            foreach (var processor in _dataProcessors)
                processor.SaveLoadedData();
        }

        private IGameplayWinUI CreateWinUI()
        {
            var obj = Object.Instantiate(_winUiPrefab);

            _winUIAnimator = obj.GetComponent<WinUIAnimator>();
            _game.LoadedLevel.OnFinishedWithTime += _winUIAnimator.SetElapsedTime;

            return obj.GetComponent<IGameplayWinUI>().HideFluent();
        }

        private void SetBestTime(IGameplayUI ui, string levelName)
        {
            var bestTime = _levelBestTimeService.GetBestTime(levelName);

            if (bestTime.IsEmpty)
                ui.HideBestTime();
            else
                ui.SetBestTime(bestTime.AsSeconds);
        }
        
        private void UpdateBestTime(float timeElapsed)
        {
            var name = _levelDescriptors[_loadedLevel].LevelName;
            
            var previous = _levelBestTimeService.GetBestTime(name);
            
            if (timeElapsed < previous.AsSeconds || previous.IsEmpty)
                _levelBestTimeService.SetBestTime(name, timeElapsed);
        }

        private static T UIFactory<T>(GameObject prefab)
            where T : IGameUI =>
            Object.Instantiate(prefab).GetComponent<T>();

        private static T HiddenUIFactory<T>(GameObject prefab)
            where T : IShowableGameUI =>
            UIFactory<T>(prefab).HideFluent();
    }
}