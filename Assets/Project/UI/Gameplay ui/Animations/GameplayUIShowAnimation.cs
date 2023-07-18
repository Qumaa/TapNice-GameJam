using DG.Tweening;

namespace Project.UI.Animation
{
    public class GameplayUIShowAnimation : DoTweenShowableUIAnimation
    {
        private readonly GameplayUIFader _ui;
        private readonly float _duration;

        public GameplayUIShowAnimation(GameplayUIFader ui, float duration)
        {
            _ui = ui;
            _duration = duration;
            CreateSequence();
        }

        protected override void FillSequence(Sequence seq)
        {
            var fadeTween = DOTween.To(
                    _ui.GetFade,
                    _ui.SetFade,
                    1,
                    _duration)
                .SetEase(Ease.InSine);

            seq.Append(fadeTween);
        }
    }
}