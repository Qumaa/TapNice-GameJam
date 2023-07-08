using Project.Game;

namespace Project.Architecture
{
    public readonly struct GameStateMachineDirector : IGameStateMachineDirector
    {
        private readonly IGame _game;
        private readonly PlayerConfig _playerConfig;
        private readonly ILevelDescriptor[] _levels;
        private readonly ISceneLoader _sceneLoader;
        private readonly IEffectsManager _effectsManager;

        public GameStateMachineDirector(IGame game, PlayerConfig playerConfig, ILevelDescriptor[] levels,
            ISceneLoader sceneLoader,
            IEffectsManager effectsManager)
        {
            _game = game;
            _playerConfig = playerConfig;
            _levels = levels;
            _sceneLoader = sceneLoader;
            _effectsManager = effectsManager;
        }

        public void Build(IGameStateMachine machine)
        {
            machine.AddState(new BootState(_game, machine, _playerConfig, _effectsManager))
                .AddState(new MenuState(_game, machine, _sceneLoader, _levels))
                .AddState(new LoadLevelState(_game, machine, _sceneLoader))
                .AddState(new LevelInitState(_game, machine));
        }
    }
}