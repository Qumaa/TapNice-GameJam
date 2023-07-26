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
        private readonly ILevelResolver _levelResolver;
        private readonly GameObject _gameplayUiPrefab;
        private readonly GameObject _pauseUiPrefab;
        private readonly GameObject _winUiPrefab;
        private readonly IPersistentDataSaver[] _dataProcessors;

        private WinUIAnimator _winUIAnimator;

        public GameplayInitializer(IGame game, IGameStateMachine stateMachine, UIConfig uiConfig,
            ILevelDescriptor[] levelDescriptors, ILevelBestTimeService levelBestTimeService,
            ILevelResolver levelResolver, params IPersistentDataSaver[] dataProcessors)
        {
            _game = game;
            _stateMachine = stateMachine;
            _levelDescriptors = levelDescriptors;
            _levelBestTimeService = levelBestTimeService;
            _levelResolver = levelResolver;
            _gameplayUiPrefab = uiConfig.GameUiPrefab;
            _pauseUiPrefab = uiConfig.PauseUiPrefab;
            _winUiPrefab = uiConfig.WinUiPrefab;
            _dataProcessors = dataProcessors;
        }

        public void InitializeGameplay(int levelToLoad) =>
            _stateMachine.SetState<LoadLevelState, RichLoadLevelArgument>(new RichLoadLevelArgument(levelToLoad, Init));

        private void Init()
        {
            _game.Player.Activate();
            _game.InputService.OnScreenTouchInput += _game.Player.JumpIfPossible;
            _game.LoadedLevel.OnFinishedWithTime += UpdateBestTime;

            var ui = HiddenUIFactory<IGameplayUI>(_gameplayUiPrefab);
            _game.UI.Add(ui);
            _game.UI.Add(HiddenUIFactory<IGameplayPauseUI>(_pauseUiPrefab));
            _game.UI.Add(CreateWinUI());

            _game.LoadedLevel.OnStarted += UpdateUI;
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

            _game.LoadedLevel.OnStarted -= UpdateUI;
        }

        private IGameplayWinUI CreateWinUI()
        {
            var obj = Object.Instantiate(_winUiPrefab);

            _winUIAnimator = obj.GetComponent<WinUIAnimator>();
            _game.LoadedLevel.OnFinishedWithTime += _winUIAnimator.SetElapsedTime;

            return obj.GetComponent<IGameplayWinUI>().HideFluent();
        }

        private void UpdateUI()
        {
            var loadedLevel = _levelResolver.CurrentLevel;
            
            var ui = _game.UI.Get<IGameplayUI>();
            var levelName = _levelDescriptors[loadedLevel].LevelName;
            
            ui.SetLevelName(loadedLevel + 1, levelName);
            SetBestTime(ui, loadedLevel);
        }

        private void SetBestTime(IGameplayUI ui, int levelIndex)
        {
            var bestTime = _levelBestTimeService.GetBestTime(levelIndex);

            if (bestTime.IsEmpty)
                ui.HideBestTime();
            else
                ui.SetBestTime(bestTime.AsSeconds);
        }
        
        private void UpdateBestTime(float timeElapsed)
        {
            var levelName = _levelResolver.CurrentLevel;
            
            var previous = _levelBestTimeService.GetBestTime(levelName);
            
            if (timeElapsed < previous.AsSeconds || previous.IsEmpty)
                _levelBestTimeService.SetBestTime(levelName, timeElapsed);
        }

        private static T UIFactory<T>(GameObject prefab)
            where T : IGameUI =>
            Object.Instantiate(prefab).GetComponent<T>();

        private static T HiddenUIFactory<T>(GameObject prefab)
            where T : IShowableGameUI =>
            UIFactory<T>(prefab).HideFluent();
    }
}