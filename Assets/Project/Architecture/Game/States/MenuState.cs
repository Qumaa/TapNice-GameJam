using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public class MenuState : GameState
    {
        private readonly ISceneLoader _sceneLoader;
        private IMainMenu _mainMenu;
        private readonly ILevelDescriptor[] _levels;

        public MenuState(IGame game, IGameStateMachine stateMachine, ISceneLoader sceneLoader,
            ILevelDescriptor[] gameLevels) :
            base(game, stateMachine)
        {
            _sceneLoader = sceneLoader;
            _levels = gameLevels;
        }

        public override void Enter()
        {
            _sceneLoader.LoadSceneHandled(1, InitMenu);
        }

        private void InitMenu()
        {
            _mainMenu = GameObject.FindGameObjectWithTag(Tags.MAIN_MENU).GetComponent<IMainMenu>();
            _mainMenu.SetLevels(_levels);

            _mainMenu.OnLevelPlayPressed += LoadLevel;
        }

        private void LoadLevel(int level) =>
            _stateMachine.SetState<LoadLevelState, int>(_levels[level].SceneIndex);

        public override void Exit()
        {
            Clean();
        }

        private void Clean()
        {
            _mainMenu.OnLevelPlayPressed -= LoadLevel;
            _mainMenu = null;
        }
    }
}