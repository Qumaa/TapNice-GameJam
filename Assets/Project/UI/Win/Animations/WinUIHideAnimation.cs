using DG.Tweening;

namespace Project.UI.Animation
{
    public class WinUIHideAnimation : DoTweenFadeUIAnimation
    {
        public WinUIHideAnimation(IFadeableUI ui, float duration) : base(ui, duration) { }
        protected override void FillSequence(float animationDuration, Sequence seq)
        {
            var fadeTween = GetLinearHideTween(animationDuration);

            seq.Append(fadeTween);
        }
    }
}