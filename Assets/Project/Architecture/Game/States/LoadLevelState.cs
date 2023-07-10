using Project.Game;

namespace Project.Architecture
{
    public class LoadLevelState : ExitableGameState, IEnterableGameState<int>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly ILevelDescriptor[] _levels;

        public LoadLevelState(IGame game, IGameStateMachine stateMachine, ISceneLoader sceneLoader,
            ILevelDescriptor[] levels) :
            base(game, stateMachine)
        {
            _sceneLoader = sceneLoader;
            _levels = levels;
        }

        public void Enter(int levelIndex) =>
            _sceneLoader.LoadSceneHandled(LevelIndexToScene(levelIndex), MoveNext);

        public override void Exit()
        {
        }

        private void MoveNext() =>
            _stateMachine.SetState<LevelInitState>();

        private int LevelIndexToScene(int levelIndex) =>
            _levels[levelIndex].SceneIndex;
    }
}