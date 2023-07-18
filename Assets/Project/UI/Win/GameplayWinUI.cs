using System;
using Project.UI.Animation;
using Project.UI.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    [RequireComponent(typeof(IShowableUIAnimator))]
    public class GameplayWinUI : ShowableGameUI, IGameplayWinUI
    {
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _quitLevelButton;
        [SerializeField] private TextMeshProUGUI _scoreText;
        private IShowableUIAnimator _animator;

        public event Action OnNextLevelPressed;
        public event Action OnRestartPressed;
        public event Action OnQuitLevelPressed;

        public void SetNextLevelButtonAvailability(bool availability)
        {
            if (availability)
                EnableNextLevelButton();
            else
                DisableNextLevelButton();
        }

        public void SetElapsedTime(float time) =>
            _scoreText.text = UIUtils.FormatTime(time);

        public void SetHighestTime(float highestTime) { }

        protected override void Awake()
        {
            base.Awake();
            _nextLevelButton.onClick.AddListener(InvokeNextLevelEvent);
            _restartButton.onClick.AddListener(InvokeRestartEvent);
            _quitLevelButton.onClick.AddListener(InvokeQuitEvent);

            _animator = GetComponent<IShowableUIAnimator>();
        }

        protected override void OnDelete()
        {
            _nextLevelButton.onClick.RemoveListener(InvokeNextLevelEvent);
            _restartButton.onClick.RemoveListener(InvokeRestartEvent);
            _quitLevelButton.onClick.RemoveListener(InvokeQuitEvent);
        }

        private void EnableNextLevelButton() =>
            _nextLevelButton.interactable = true;

        private void DisableNextLevelButton() =>
            _nextLevelButton.interactable = false;

        private void InvokeNextLevelEvent() =>
            OnNextLevelPressed?.Invoke();

        private void InvokeRestartEvent() =>
            OnRestartPressed?.Invoke();

        private void InvokeQuitEvent() =>
            OnQuitLevelPressed?.Invoke();

        public void ShowAnimated()
        {
            base.Show();
            _animator.PlayShowingAnimation();
        }

        public void HideAnimated() =>
            _animator.PlayHidingAnimationHandled(base.Hide);
    }
}