using System;
using Project.Architecture.SceneManagement;
using Project.Game.Levels;
using UnityEngine;

namespace Project.Architecture.States
{
    public class LoadLevelState : GameState<int>, IEnterableGameState<RichLoadLevelArgument>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly ILevelDescriptor[] _levels;
        private readonly ILevelResolver _levelResolver;

        public LoadLevelState(IGame game, IGameStateMachine stateMachine, ISceneLoader sceneLoader,
            ILevelDescriptor[] levels, ILevelResolver levelResolver) :
            base(game, stateMachine)
        {
            _sceneLoader = sceneLoader;
            _levels = levels;
            _levelResolver = levelResolver;
        }

        public override void Enter(int levelIndex)
        {
            EnterInternal(levelIndex, Callback);

            void Callback() =>
                MoveNext(LevelIndexToName(levelIndex));
        }

        public void Enter(RichLoadLevelArgument arg)
        {
            EnterInternal(
                arg.LevelIndex,
                Callback
            );

            void Callback()
            {
                arg.LoadedCallback();
                MoveNext(LevelIndexToName(arg.LevelIndex));
            }
        }

        public override void Exit() { }

        private void MoveNext(string levelName) =>
            _stateMachine.SetState<LevelInitState, string>(levelName);

        private int LevelIndexToScene(int levelIndex) =>
            _levels[levelIndex].SceneIndex;

        private string LevelIndexToName(int levelIndex) =>
            _levels[levelIndex].LevelName;

        private void EnterInternal(int levelIndex, Action callback)
        {
            _levelResolver.SetLevel(levelIndex);
            _sceneLoader.LoadSceneHandled(LevelIndexToScene(levelIndex), callback);
        }
    }
}