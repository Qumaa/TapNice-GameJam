using Project.Architecture.SceneManagement;
using Project.Game.Levels;
using Project.UI;
using UnityEngine;

namespace Project.Architecture.States
{
    public class MenuState : GameState
    {
        private readonly ISceneLoader _sceneLoader;
        private IMainMenu _mainMenu;
        private readonly ILevelDescriptor[] _levels;
        private readonly GameObject _menuPrefab;
        private readonly ILevelUnlocker _levelUnlocker;

        public MenuState(IGame game, IGameStateMachine stateMachine, ISceneLoader sceneLoader,
            ILevelDescriptor[] gameLevels, GameObject menuPrefab, ILevelUnlocker levelUnlocker) :
            base(game, stateMachine)
        {
            _sceneLoader = sceneLoader;
            _levels = gameLevels;
            _menuPrefab = menuPrefab;
            _levelUnlocker = levelUnlocker;
        }

        public override void Enter() =>
            _sceneLoader.LoadSceneHandled(1, LoadMainMenu);

        public override void Exit() =>
            UnloadMainMenu();

        private void LoadLevel(int level) =>
            _stateMachine.SetState<GameplayState, int>(level);

        private void LoadMainMenu()
        {
            _mainMenu = Object.Instantiate(_menuPrefab).GetComponent<IMainMenu>();
            _mainMenu.SetLevels(_levels, _levelUnlocker);

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
    }
}