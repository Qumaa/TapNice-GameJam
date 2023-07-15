using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class GameplayWinUI : ShowableGameUI, IGameplayWinUI
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _quitLevelButton;
        
        public event Action OnNextLevelPressed;
        public event Action OnRestartPressed;
        public event Action OnQuitLevelPressed;

        protected override void Awake()
        {
            base.Awake();
            _resumeButton.onClick.AddListener(InvokeNextLevelEvent);
            _restartButton.onClick.AddListener(InvokeRestartEvent);
            _quitLevelButton.onClick.AddListener(InvokeQuitEvent);
        }

        protected override void OnDelete()
        {
            _resumeButton.onClick.RemoveListener(InvokeNextLevelEvent);
            _restartButton.onClick.RemoveListener(InvokeRestartEvent);
            _quitLevelButton.onClick.RemoveListener(InvokeQuitEvent);
        }

        private void InvokeNextLevelEvent() =>
            OnNextLevelPressed?.Invoke();

        private void InvokeRestartEvent() =>
            OnRestartPressed?.Invoke();

        private void InvokeQuitEvent() =>
            OnQuitLevelPressed?.Invoke();
    }
}