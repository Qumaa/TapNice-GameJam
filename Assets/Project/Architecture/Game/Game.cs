using System.Collections.Generic;
using Project.Game;

namespace Project.Architecture
{
    public class Game : IGame
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly List<IUpdatable> _updatables;
        private readonly List<IFixedUpdatable> _fixedUpdatables;
        
        public ICameraController CameraController { get; set; }
        public IPlayerInputService InputService { get; set; }
        public IPlayer Player { get; set; }

        public Game(PlayerConfig playerConfig)
        {
            _updatables = new List<IUpdatable>();
            _fixedUpdatables = new List<IFixedUpdatable>();
            
            _stateMachine = new GameStateMachine();
            InitializeStates(playerConfig);
            Start();
        }

        private void Start() =>
            _stateMachine.SetState<BootState>();

        private void InitializeStates(PlayerConfig playerConfig)
        {
            var director = new GameStateMachineDirector(this, playerConfig, new SyncSceneLoader(new SceneLoadingOperationHandler()), new EffectsManager());
            
            director.Build(_stateMachine);
        }

        public void Update(float timeStep)
        {
            foreach(var updatable in _updatables)
                updatable.Update(timeStep);
        }

        public void FixedUpdate(float fixedTimeStep)
        {
            foreach(var fixedUpdatable in _fixedUpdatables)
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