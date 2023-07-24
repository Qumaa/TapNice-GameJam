using Project.Architecture.SceneManagement;
using Project.Configs;
using Project.Game.CollisionHandling;
using Project.Game.Levels;

namespace Project.Architecture.States
{
    public class GameplayState : GameState<int>, IGameplayLeaver
    {
        private readonly GameplayInitializer _gameplayInitializer;
        private readonly IGameStateMachine _internalStateMachine;

        public GameplayState(IGame game, IGameStateMachine stateMachine, ISceneLoader sceneLoader,
            ILevelDescriptor[] levels, INextLevelResolver nextLevelResolver, ICollisionHandlerResolver handlerResolver, 
            UIConfig uiConfig, ILevelBestTimeService levelBestTimeService, ILevelUnlocker levelUnlocker) :
            base(game, stateMachine)
        {
            _internalStateMachine = new GameStateMachine();

            new GameplayStateMachineDirector(
                game,
                sceneLoader,
                levels,
                nextLevelResolver,
                handlerResolver,
                levelUnlocker,
                this
            ).Build(_internalStateMachine);

            _gameplayInitializer = new GameplayInitializer(
                game,
                _internalStateMachine,
                uiConfig,
                levelBestTimeService,
                levelUnlocker
            );
        }

        public override void Enter(int levelToEnter) =>
            _gameplayInitializer.InitializeGameplay(levelToEnter);

        public override void Exit() =>
            _gameplayInitializer.KillGameplay();

        void IGameplayLeaver.LeaveToMainMenu()
        {
            _internalStateMachine.ExitCurrentState();
            _stateMachine.SetState<MenuState>();
        }
    }
}