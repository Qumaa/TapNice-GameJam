namespace Project.Architecture
{
    public class LoadLevelState : ExitableGameState, IEnterableGameState<string>, IEnterableGameState<int>
    {
        private readonly ISceneLoader _sceneLoader;
        private ISceneLoadingOperation _loadingOperation;
        
        public LoadLevelState(IGame game, IGameStateMachine stateMachine, ISceneLoader sceneLoader) : 
            base(game, stateMachine)
        {
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName) =>
            HandleLoading(_sceneLoader.LoadScene(sceneName));

        public void Enter(int sceneIndex) =>
            HandleLoading(_sceneLoader.LoadScene(sceneIndex));

        public override void Exit()
        {
        }

        private void HandleLoading(ISceneLoadingOperation loadingOperation)
        {
            if (loadingOperation.IsDone)
            {
                MoveNext();
                return;
            }

            _loadingOperation = loadingOperation;
            _loadingOperation.OnLoadingCompleted += HandleLoadingCompleted;
        }

        private void HandleLoadingCompleted()
        {
            _loadingOperation.OnLoadingCompleted -= HandleLoadingCompleted;
            _loadingOperation = null;
            MoveNext();
        }

        private void MoveNext() =>
            _stateMachine.SetState<LevelInitState>();
    }
}