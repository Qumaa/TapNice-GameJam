using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class GameplayPauseUI : ShowableGameUI, IGameplayPauseUI
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _quitLevelButton;
        
        public event Action OnResumePressed;
        public event Action OnRestartPressed;
        public event Action OnQuitLevelPressed;

        protected override void Awake()
        {
            base.Awake();
            _resumeButton.onClick.AddListener(InvokeResumeEvent);
            _restartButton.onClick.AddListener(InvokeRestartEvent);
            _quitLevelButton.onClick.AddListener(InvokeQuitEvent);
        }

        protected override void OnDelete()
        {
            _resumeButton.onClick.RemoveListener(InvokeResumeEvent);
            _restartButton.onClick.RemoveListener(InvokeRestartEvent);
            _quitLevelButton.onClick.RemoveListener(InvokeQuitEvent);
        }

        private void InvokeResumeEvent() =>
            OnResumePressed?.Invoke();

        private void InvokeRestartEvent() =>
            OnRestartPressed?.Invoke();

        private void InvokeQuitEvent() =>
            OnQuitLevelPressed?.Invoke();
    }
}