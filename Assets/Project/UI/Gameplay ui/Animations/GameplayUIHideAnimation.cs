using DG.Tweening;

namespace Project.UI.Animation
{
    public class GameplayUIHideAnimation : DoTweenFadeUIAnimation
    {
        public GameplayUIHideAnimation(IFadeableUI ui, float duration) : base(ui, duration) { }
        protected override void FillSequence(float animationDuration, Sequence seq)
        {
            var fadeTween = DOTween.To(
                GetUIFade,
                SetUIFade,
                0,
                animationDuration
            );

            seq.Append(fadeTween);
        }
    }
}