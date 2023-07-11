using System.Collections.Generic;
using Project.Game;
using Project.UI;

namespace Project.Architecture
{
    public class Game : IGame
    {
        private readonly IApplicationQuitter _applicationQuitter;
        private readonly ILevelDescriptor[] _levels;
        private readonly IGameStateMachine _stateMachine;
        private readonly List<IUpdatable> _updatables;
        private readonly List<IFixedUpdatable> _fixedUpdatables;
        private readonly ISceneLoader _sceneLoader;
        private int _loadedLevel;

        public IGameInputService InputService { get; set; }
        public IPlayer Player { get; set; }
        public ILevel LoadedLevel { get; set; }
        public IGameUIRenderer UI { get; set; }

        public Game(PlayerConfig playerConfig, UIConfig uiConfig, ILevelDescriptor[] levels,
            IApplicationQuitter applicationQuitter)
        {
            _levels = levels;
            _applicationQuitter = applicationQuitter;
            _updatables = new List<IUpdatable>();
            _fixedUpdatables = new List<IFixedUpdatable>();
            _sceneLoader = new SyncSceneLoader(new SceneLoadingOperationHandler());

            _stateMachine = new GameStateMachine();
            InitializeStates(playerConfig, uiConfig, levels);
        }

        public void Start() =>
            _stateMachine.SetState<BootState>();

        public void LoadLevel(int index)
        {
            _loadedLevel = index;
            _stateMachine.SetState<LoadLevelState, int>(_loadedLevel);
        }

        public bool LoadNextLevel()
        {
            if (++_loadedLevel >= _levels.Length)
                return false;

            LoadLevel(_loadedLevel);
            return true;
        }

        public void LoadMainMenu() =>
            _stateMachine.SetState<MenuState>();

        public void Quit() =>
            _applicationQuitter.Quit();

        private void InitializeStates(PlayerConfig playerConfig, UIConfig uiConfig, ILevelDescriptor[] gameLevelsConfig)
        {
            var director = new GameStateMachineDirector(this, gameLevelsConfig, _sceneLoader, uiConfig);

            var bootState = new BootState(this, _stateMachine, playerConfig, new EffectsManager(), director,
                uiConfig.CanvasPrefab);

            _stateMachine.AddState(bootState);
        }

        public void Update(float timeStep)
        {
            foreach (var updatable in _updatables)
                updatable.Update(timeStep);
        }

        public void FixedUpdate(float fixedTimeStep)
        {
            foreach (var fixedUpdatable in _fixedUpdatables)
                fixedUpdatable.FixedUpdate(fixedTimeStep);
        }

        public void Add(IUpdatable item) =>
            _updatables.Add(item);

        public void Remove(IUpdatable item) =>
            _updatables.Remove(item);

        public void Add(IFixedUpdatable item) =>
            _fixedUpdatables.Add(item);

        public void Remove(IFixedUpdatable item) =>
            _fixedUpdatables.Remove(item);
    }
}