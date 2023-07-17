using System;
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

        public void SetHighestTime(float highestTime) { }

        private void EmitPauseEvent() =>
            OnPausePressed?.Invoke();

        public override void Hide()
        {
            
        }

        public override void Show()
        {
            
        }
    }
}