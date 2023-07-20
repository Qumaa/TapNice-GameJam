using Project.Game.VFX;
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
        private readonly IBackgroundParticles _backgroundParticles;

        public InitGameLoopState(IGame game, IGameStateMachine stateMachine, GameObject gameplayUiPrefab,
            GameObject pauseUiPrefab, GameObject winUiPrefab, IBackgroundParticles backgroundParticles) : base(game, stateMachine)
        {
            _gameplayUiPrefab = gameplayUiPrefab;
            _pauseUiPrefab = pauseUiPrefab;
            _winUiPrefab = winUiPrefab;
            _backgroundParticles = backgroundParticles;
        }

        public override void Enter(int arg) =>
            LoadLevel(arg);

        private void LoadLevel(int level) =>
            _stateMachine.SetState<LoadLevelState, RichLoadLevelArgument>(new RichLoadLevelArgument(level, Init));

        private void Init()
        {
            _game.Player.Activate();
            _game.LoadedLevel.OnStarted += _backgroundParticles.Reset;
            _backgroundParticles.Activate();

            CalculateLevelBorders(out var lower, out var upper);
            _backgroundParticles.Init(lower, upper);
            _game.Add(_backgroundParticles);
            CreateUI();
        }

        private static void CalculateLevelBorders(out Vector2 lower, out Vector2 upper)
        {
            var objects = GameObject.FindGameObjectsWithTag(Tags.SEGMENT_MARKER);
            lower = upper = objects[0].transform.position;

            for (var i = 1; i < objects.Length; i++)
            {
                var pos = objects[i].transform.position;

                if (pos.x < lower.x || pos.y < lower.y)
                    lower = pos;

                if (pos.x > upper.x || pos.y > upper.y)
                    upper = pos;
            }
        }

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

        private static T UIFactory<T>(GameObject prefab) where T : IGameUI =>
            Object.Instantiate(prefab).GetComponent<T>();

        private static T HiddenUIFactory<T>(GameObject prefab) where T : IShowableGameUI =>
            UIFactory<T>(prefab).HideFluent();
    }
}