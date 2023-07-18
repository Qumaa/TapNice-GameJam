using System;
using Project.UI.Animation;
using Project.UI.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    [RequireComponent(typeof(IShowableUIAnimator))]
    public class GameplayUI : ShowableGameUI, IGameplayUI, IFadeableUI
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private TextMeshProUGUI _timeLabel;
        private IShowableUIAnimator _animator;
        private GameplayUIFader _fader;

        public float Fade
        {
            get => _fader.Fade;
            set => _fader.Fade = value;
        }

        public event Action OnPausePressed;

        protected override void Awake()
        {
            base.Awake();
            _pauseButton.onClick.AddListener(EmitPauseEvent);
            _animator = GetComponent<IShowableUIAnimator>();
            _fader = new GameplayUIFader(_pauseButton, _timeLabel);
        }

        protected override void OnDelete() =>
            _pauseButton.onClick.RemoveListener(EmitPauseEvent);

        public void DisplayTime(float time) =>
            _timeLabel.text = UIUtils.FormatTime(time);

        public void SetHighestTime(float highestTime) { }

        public void ShowAnimated()
        {
            base.Show();
            _animator.PlayShowingAnimation();
        }

        public void HideAnimated() =>
            _animator.PlayHidingAnimationHandled(base.Hide);

        private void EmitPauseEvent() =>
            OnPausePressed?.Invoke();
    }
}