using Project.Game;
using Project.UI;
using UnityEngine;

namespace Project.Architecture
{
    public class MenuState : GameState
    {
        private readonly ISceneLoader _sceneLoader;
        private IMainMenu _mainMenu;
        private readonly ILevelDescriptor[] _levels;
        private readonly GameObject _menuPrefab;
        private readonly GameObject _gameUiPrefab;

        public MenuState(IGame game, IGameStateMachine stateMachine, ISceneLoader sceneLoader,
            ILevelDescriptor[] gameLevels, GameObject menuPrefab, GameObject gameUiPrefab) :
            base(game, stateMachine)
        {
            _sceneLoader = sceneLoader;
            _levels = gameLevels;
            _menuPrefab = menuPrefab;
            _gameUiPrefab = gameUiPrefab;
        }

        public override void Enter()
        {
            _game.Player.Deactivate();
            _sceneLoader.LoadSceneHandled(1, InitMenu);
        }

        public override void Exit()
        {
            _game.Player.Activate();

            UnloadMainMenu();
        }

        private void InitMenu()
        {
            LoadMainMenu();
        }

        private void LoadLevel(int level) =>
            _game.LoadLevel(level);

        private void LoadMainMenu()
        {
            _mainMenu = CreateMenu<IMainMenu>(_menuPrefab);
            _mainMenu.SetLevels(_levels);

            _mainMenu.OnQuitPressed += _game.Quit;
            _mainMenu.OnLevelPlayPressed += LoadLevel;

            _game.UI.Add(_mainMenu);
        }

        private void UnloadMainMenu()
        {
            _mainMenu.OnQuitPressed -= _game.Quit;
            _mainMenu.OnLevelPlayPressed -= LoadLevel;

            _game.UI.Remove(_mainMenu);
        }

        private void LoadGameUI()
        {
            // todo:
        }

        private void UnloadGameUI()
        {
            // todo:
        }

        private static T CreateMenu<T>(GameObject prefab) =>
            Object.Instantiate(prefab).GetComponent<T>();
    }
}