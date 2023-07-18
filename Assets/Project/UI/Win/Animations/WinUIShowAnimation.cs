using DG.Tweening;

namespace Project.UI.Animation
{
    public class WinUIShowAnimation : DoTweenShowableUIAnimation
    {
        private readonly WinUIAnimator _animator;
        private readonly float _duration;
        private readonly GameplayWinUI _ui;
        private readonly ImageFadeWrapper _background;
        private readonly TextFadeWrapper _scoreText;
        private readonly TextFadeWrapper _scoreLabel;
        private readonly ButtonFadeWrapper _nextLevelButton;
        private readonly ButtonFadeWrapper _restartButton;
        private readonly ButtonFadeWrapper _quitButton;

        private float _fade; // temp
        private float _elapsedTime;

        public WinUIShowAnimation(WinUIAnimator animator, float duration, GameplayWinUI ui,
            ImageFadeWrapper background, TextFadeWrapper scoreText,
            TextFadeWrapper scoreLabel, ButtonFadeWrapper nextLevelButton, ButtonFadeWrapper restartButton,
            ButtonFadeWrapper quitButton)
        {
            _animator = animator;
            _duration = duration;
            _ui = ui;
            _background = background;
            _scoreText = scoreText;
            _scoreLabel = scoreLabel;
            _nextLevelButton = nextLevelButton;
            _restartButton = restartButton;
            _quitButton = quitButton;
        }

        public override void Play()
        {
            CreateSequence();
            SetElapsedTime(0);
            SetFade(0);
            base.Play();
        }

        protected override void FillSequence(Sequence seq)
        {
            var fadeTween = DOTween.To(
                GetFade,
                SetFade,
                1,
                _duration
            ).OnComplete(InvokeEnded);

            var scoreTween = DOTween.To(
                GetElapsedTime,
                SetElapsedTime,
                _animator.GetElapsedTime(),
                _duration / 2f
            );

            seq.Append(fadeTween).Insert(_duration / 2f, scoreTween);
        }

        private float GetFade() =>
            _fade;

        private void SetFade(float fade)
        {
            _fade = fade;

            _background.SetFade(fade);
            _scoreText.SetFade(fade);
            _scoreLabel.SetFade(fade);
            _nextLevelButton.SetFade(fade);
            _restartButton.SetFade(fade);
            _quitButton.SetFade(fade);
        }

        private float GetElapsedTime() =>
            _elapsedTime;

        private void SetElapsedTime(float time)
        {
            _elapsedTime = time;
            _ui.SetElapsedTime(time);
        }
    }
}