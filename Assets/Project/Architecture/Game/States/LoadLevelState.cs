using Project.Game;

namespace Project.Architecture
{
    public class LoadLevelState : GameState<int>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly ILevelDescriptor[] _levels;
        private readonly INextLevelResolver _nextLevelResolver;

        public LoadLevelState(IGame game, IGameStateMachine stateMachine, ISceneLoader sceneLoader,
            ILevelDescriptor[] levels, INextLevelResolver nextLevelResolver) :
            base(game, stateMachine)
        {
            _sceneLoader = sceneLoader;
            _levels = levels;
            _nextLevelResolver = nextLevelResolver;
        }

        public override void Enter(int levelIndex)
        {
            _nextLevelResolver.SetLevel(levelIndex);
            _sceneLoader.LoadSceneHandled(LevelIndexToScene(levelIndex), MoveNext);
        }

        public override void Exit()
        {
        }

        private void MoveNext() =>
            _stateMachine.SetState<LevelInitState>();

        private int LevelIndexToScene(int levelIndex) =>
            _levels[levelIndex].SceneIndex;
    }
}