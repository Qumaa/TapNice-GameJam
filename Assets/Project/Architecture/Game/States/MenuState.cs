using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public class MenuState : GameState
    {
        private readonly ISceneLoader _sceneLoader;
        private IMainMenu _mainMenu;

        public MenuState(IGame game, IGameStateMachine stateMachine, ISceneLoader sceneLoader) :
            base(game, stateMachine)
        {
            _sceneLoader = sceneLoader;
        }

        public override void Enter()
        {
            _sceneLoader.LoadSceneHandled(1, InitMenu);
        }

        private void InitMenu()
        {
            _mainMenu = GameObject.FindGameObjectWithTag(Tags.MAIN_MENU).GetComponent<IMainMenu>();

            _mainMenu.OnLevelPlayPressed += LoadLevel;
        }

        private void LoadLevel(int level) =>
            _stateMachine.SetState<LoadLevelState, int>(level + 2);

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