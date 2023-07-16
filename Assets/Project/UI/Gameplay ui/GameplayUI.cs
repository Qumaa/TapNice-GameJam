using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class GameplayUI : ShowableGameUI, IGameplayUI
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private TextMeshProUGUI _timeLabel;

        public event Action OnPausePressed;

        protected override void Awake()
        {
            base.Awake();
            _pauseButton.onClick.AddListener(EmitPauseEvent);
        }

        protected override void OnDelete() =>
            _pauseButton.onClick.RemoveListener(EmitPauseEvent);

        public void DisplayTime(float time) =>
            _timeLabel.text = FormatTime(time);

        public void SetHighestTime(float highestTime) { }

        private void EmitPauseEvent() =>
            OnPausePressed?.Invoke();

        private static string FormatTime(float time) =>
            time.ToString("F2");
    }
}