namespace Project.Architecture
{
    public class LoadLevelState : ExitableGameState, IEnterableGameState<string>, IEnterableGameState<int>
    {
        private readonly ISceneLoader _sceneLoader;
        
        public LoadLevelState(IGame game, IGameStateMachine stateMachine, ISceneLoader sceneLoader) : 
            base(game, stateMachine)
        {
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.LoadScene(sceneName);
            MoveNext();
        }

        public void Enter(int sceneIndex)
        {
            _sceneLoader.LoadScene(sceneIndex);
            MoveNext();
        }

        public override void Exit()
        {
        }

        private void MoveNext() =>
            _stateMachine.SetState<LevelInitState>();
    }
}