using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Animation
{
    [RequireComponent(typeof(GameplayWinUI))]
    public class WinUIAnimator : ShowableUIAnimator
    {
        [SerializeField] private Image _background;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _scoreLabel;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _quitButton;
        private float _time;

        protected override void Awake()
        {
            base.Awake();

            var ui = GetComponent<GameplayWinUI>();
            var background = new ImageFadeWrapper(_background);
            var scoreText = new TextFadeWrapper(_scoreText);
            var scoreLabel = new TextFadeWrapper(_scoreLabel);
            var nextLevelButton = new ButtonFadeWrapper(_nextLevelButton);
            var restartButton = new ButtonFadeWrapper(_restartButton);
            var quitButton = new ButtonFadeWrapper(_quitButton);

            _showAnimation = new WinUIShowAnimation(this, _showDuration, ui, background, scoreText, scoreLabel, nextLevelButton,
                restartButton, quitButton);
        }

        public void SetElapsedTime(float time) =>
            _time = time;

        public float GetElapsedTime() =>
            _time;
    }
}