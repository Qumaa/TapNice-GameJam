using DG.Tweening;

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
    }
}