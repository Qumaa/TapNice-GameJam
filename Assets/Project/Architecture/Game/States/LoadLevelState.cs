using System;
using Project.Architecture.SceneManagement;
using Project.Game.Levels;

namespace Project.Architecture.States
{
    public class LoadLevelState : GameState<int>, IEnterableGameState<RichLoadLevelArgument>
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

        public override void Enter(int levelIndex) =>
            EnterInternal(levelIndex, MoveNext);

        public void Enter(RichLoadLevelArgument arg) =>
            EnterInternal(arg.LevelIndex, () => { arg.LoadedCallback(); MoveNext(); } );

        public override void Exit()
        {
        }

        private void MoveNext() =>
            _stateMachine.SetState<LevelInitState>();

        private int LevelIndexToScene(int levelIndex) =>
            _levels[levelIndex].SceneIndex;

        private void EnterInternal(int levelIndex, Action callback)
        {
            _nextLevelResolver.SetLevel(levelIndex);
            _sceneLoader.LoadSceneHandled(LevelIndexToScene(levelIndex), callback);
        }
    }
}