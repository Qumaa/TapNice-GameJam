﻿using Project.UI;
using UnityEngine;

namespace Project.Architecture.States
{
    public class InitGameLoopState : GameState<int>
    {
        private readonly GameObject _gameplayUiPrefab;
        private readonly GameObject _pauseUiPrefab;
        private readonly GameObject _winUiPrefab;

        public InitGameLoopState(IGame game, IGameStateMachine stateMachine, GameObject gameplayUiPrefab,
            GameObject pauseUiPrefab, GameObject winUiPrefab) : base(game, stateMachine)
        {
            _gameplayUiPrefab = gameplayUiPrefab;
            _pauseUiPrefab = pauseUiPrefab;
            _winUiPrefab = winUiPrefab;
        }

        public override void Enter(int arg)
        {
            CreateUI();
            LoadLevel(arg);
        }

        private void LoadLevel(int level) =>
            _stateMachine.SetState<LoadLevelState, int>(level);

        private void CreateUI()
        {
            _game.UI.Add(HiddenUIFactory<IGameplayUI>(_gameplayUiPrefab));
            _game.UI.Add(HiddenUIFactory<IGameplayPauseUI>(_pauseUiPrefab));
            _game.UI.Add(HiddenUIFactory<IGameplayWinUI>(_winUiPrefab));
        }

        public override void Exit() { }

        private static T UIFactory<T>(GameObject prefab) where T : IGameUI =>
            Object.Instantiate(prefab).GetComponent<T>();

        private static T HiddenUIFactory<T>(GameObject prefab) where T : IShowableGameUI =>
            UIFactory<T>(prefab).HideFluent();
    }
}