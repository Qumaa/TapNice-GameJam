using System;
using DG.Tweening;

namespace Project.UI.Animation
{
    public class GameplayUIShowAnimation : DoTweenFadeUIAnimation
    {
        public GameplayUIShowAnimation(IFadeableUI ui, float duration) : base(ui, duration) { }

        protected override void FillSequence(float animationDuration, Sequence seq)
        {
            var fadeTween = DOTween.To(
                GetUIFade,
                SetUIFade,
                1,
                animationDuration
            );

            seq.Append(fadeTween);
        }
    }
}