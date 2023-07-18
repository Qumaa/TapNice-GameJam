using DG.Tweening;

namespace Project.UI.Animation
{
    public class WinUIShowAnimation : DoTweenFadeUIAnimation
    {
        public WinUIShowAnimation(IFadeableUI ui, float duration) : base(ui, duration) { }
        protected override void FillSequence(float animationDuration, Sequence seq)
        {
            var fadeTween = GetLinearShowTween(animationDuration);

            seq.Append(fadeTween);
        }
    }
}