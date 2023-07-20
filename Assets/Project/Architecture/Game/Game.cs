using System.Collections.Generic;
using Project.Architecture.SceneManagement;
using Project.Architecture.States;
using Project.Configs;
using Project.Game;
using Project.Game.Effects;
using Project.Game.Levels;
using Project.Game.Player;
using Project.UI;
using UnityEngine;

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

        public IGameInputService InputService { get; set; }
        public IPlayer Player { get; set; }
        public ILevel LoadedLevel { get; set; }
        public IGameUIRenderer UI { get; set; }

        public Game(PlayerConfig playerConfig, UIConfig uiConfig, VFXConfig vfxConfig, ILevelDescriptor[] levels,
            IApplicationQuitter applicationQuitter)
        {
            _levels = levels;
            _applicationQuitter = applicationQuitter;
            _updatables = new List<IUpdatable>();
            _fixedUpdatables = new List<IFixedUpdatable>();
            _sceneLoader = new SyncSceneLoader(new SceneLoadingOperationHandler());

            _stateMachine = new GameStateMachine();
            InitializeStates(playerConfig, uiConfig, levels, vfxConfig);
        }

        public void Start() =>
            _stateMachine.SetState<BootState>();

        public void Quit() =>
            _applicationQuitter.Quit();

        private void InitializeStates(PlayerConfig playerConfig, UIConfig uiConfig, ILevelDescriptor[] gameLevels,
            VFXConfig vfxConfig)
        {
            var bootState = new BootState(
                this,
                _stateMachine,
                playerConfig,
                new EffectsManager(),
                uiConfig,
                _sceneLoader,
                gameLevels,
                vfxConfig
            );

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

        public void Pause() =>
            Time.timeScale = 0;

        public void Resume() =>
            Time.timeScale = 1;
    }
}