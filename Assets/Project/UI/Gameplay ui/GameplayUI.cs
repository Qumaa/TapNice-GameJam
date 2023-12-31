﻿using System;
using Project.UI.Animation;
using Project.UI.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    [RequireComponent(typeof(IShowableUIAnimator))]
    public class GameplayUI : ShowableGameUI, IGameplayUI
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private TextMeshProUGUI _timeLabel;
        [SerializeField] private TextMeshProUGUI _bestTimeLabel;
        [SerializeField] private TextMeshProUGUI _levelNameLabel;
        private IShowableUIAnimator _animator;

        public event Action OnPausePressed;

        protected override void Awake()
        {
            base.Awake();
            _pauseButton.onClick.AddListener(EmitPauseEvent);
            _animator = GetComponent<IShowableUIAnimator>();
        }

        protected override void OnDelete() =>
            _pauseButton.onClick.RemoveListener(EmitPauseEvent);

        public void DisplayTime(float time) =>
            _timeLabel.text = UIUtils.FormatTime(time);

        public void SetBestTime(float bestTime)
        {
            SetBestTimeVisibility(true);
            _bestTimeLabel.text = $"Best: {UIUtils.FormatTime(bestTime)}";
        }

        public void SetLevelName(int displayIndex, string displayName) =>
            _levelNameLabel.text = $"{displayIndex}. {displayName}";

        public void HideBestTime() =>
            SetBestTimeVisibility(false);

        public void ShowAnimated()
        {
            base.Show();
            _animator.PlayShowingAnimation();
        }

        public void HideAnimated() =>
            _animator.PlayHidingAnimationHandled(base.Hide);

        private void EmitPauseEvent() =>
            OnPausePressed?.Invoke();

        private void SetBestTimeVisibility(bool visibility) =>
            _bestTimeLabel.gameObject.SetActive(visibility);
    }
}