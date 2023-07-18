using DG.Tweening;

namespace Project.UI.Animation
{
    public class GameplayUIHideAnimation : DoTweenShowableUIAnimation
    {
        private readonly GameplayUIFader _ui;
        private readonly float _duration;

        public GameplayUIHideAnimation(GameplayUIFader ui, float duration)
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
                    0,
                    _duration)
                .SetEase(Ease.OutSine);

            seq.Append(fadeTween);
        }
    }
}