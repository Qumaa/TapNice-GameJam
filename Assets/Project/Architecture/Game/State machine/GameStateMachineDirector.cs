using Project.Game;

namespace Project.Architecture
{
    public readonly struct GameStateMachineDirector : IGameStateMachineDirector
    {
        private readonly IGame _game;
        private readonly PlayerConfig _playerConfig;
        private readonly ISceneLoader _sceneLoader;
        private readonly IEffectsManager _effectsManager;

        public GameStateMachineDirector(IGame game, PlayerConfig playerConfig, ISceneLoader sceneLoader,
            IEffectsManager effectsManager)
        {
            _game = game;
            _playerConfig = playerConfig;
            _sceneLoader = sceneLoader;
            _effectsManager = effectsManager;
        }

        public void Build(IGameStateMachine machine)
        {
            machine.AddState(new BootState(_game, machine, _playerConfig, _effectsManager))
                .AddState(new MenuState(_game, machine, _sceneLoader))
                .AddState(new LoadLevelState(_game, machine, _sceneLoader))
                .AddState(new LevelInitState(_game, machine));
        }
    }
}