using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

namespace Project.UI.Animation
{
    public abstract class DoTweenFadeUIAnimation : FadeUIAnimation
    {
        private readonly Sequence _sequence;

        protected DoTweenFadeUIAnimation(IFadeableUI ui, float duration) : base(ui)
        {
            _sequence = CreateSequence(duration);
            _sequence.OnComplete(InvokeEnded);
        }

        public override void Play()
        {
            _sequence.Restart();
            _sequence.Play();
        }

        public override void Stop() =>
            _sequence.Pause();

        private Sequence CreateSequence(float duration)
        {
            var seq = DOTween.Sequence();

            FillSequence(duration, seq);

            return seq;
        }

        protected abstract void FillSequence(float animationDuration, Sequence seq);

        protected TweenerCore<float, float, FloatOptions> GetLinearFadeTween(float animationDuration, float endValue) =>
            DOTween.To(
                GetUIFade,
                SetUIFade,
                endValue,
                animationDuration
            );

        protected TweenerCore<float, float, FloatOptions> GetLinearShowTween(float animationDuration) =>
            GetLinearFadeTween(animationDuration, 1);

        protected TweenerCore<float, float, FloatOptions> GetLinearHideTween(float animationDuration) =>
            GetLinearFadeTween(animationDuration, 0);
    }
}