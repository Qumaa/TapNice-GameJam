using System.Collections.Generic;
using Project.Game;

namespace Project.Architecture
{
    public class Game : IGame
    {
        private readonly ILevelDescriptor[] _levels;
        private readonly IGameStateMachine _stateMachine;
        private readonly List<IUpdatable> _updatables;
        private readonly List<IFixedUpdatable> _fixedUpdatables;
        private readonly ISceneLoader _sceneLoader;
        private int _loadedLevel;
        
        public IGameInputService InputService { get; set; }
        public IPlayer Player { get; set; }
        public ILevel LoadedLevel { get; set; }

        public Game(PlayerConfig playerConfig, ILevelDescriptor[] levels)
        {
            _levels = levels;
            _updatables = new List<IUpdatable>();
            _fixedUpdatables = new List<IFixedUpdatable>();
            _sceneLoader = new SyncSceneLoader(new SceneLoadingOperationHandler());

            _stateMachine = new GameStateMachine();
            InitializeStates(playerConfig, levels);
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

        private void InitializeStates(PlayerConfig playerConfig, ILevelDescriptor[] gameLevelsConfig)
        {
            var director = new GameStateMachineDirector(this, gameLevelsConfig, _sceneLoader);

            _stateMachine.AddState(new BootState(this, _stateMachine, playerConfig, new EffectsManager(), director));
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