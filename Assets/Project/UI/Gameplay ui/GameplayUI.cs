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
        private float _fade;

        public float Fade
        {
            get => _fade;
            set => SetFade(value);
        }

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

        public void SetHighestTime(float highestTime) { }

        public override void Hide()
        {
            DisableInteractivity();
            _animator.PlayHidingAnimation();
        }

        public override void Show() =>
            _animator.PlayShowingAnimationHandled(EnableInteractivity);

        private void EmitPauseEvent() =>
            OnPausePressed?.Invoke();

        private void DisableInteractivity() =>
            _pauseButton.enabled = false;

        private void EnableInteractivity() =>
            _pauseButton.enabled = true;
        
        private void SetFade(float fade)
        {
            _fade = fade;
            
            var color = _pauseButton.image.color;
            color.a = fade;
            _pauseButton.image.color = color;
        }
    }
}