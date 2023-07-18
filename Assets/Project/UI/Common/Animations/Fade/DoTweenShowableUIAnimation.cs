using DG.Tweening;

namespace Project.UI.Animation
{
    public abstract class DoTweenShowableUIAnimation : ShowableUIAnimation
    {
        private Sequence _sequence;

        public override void Play()
        {
            _sequence.Restart();
            _sequence.Play();
        }

        public override void Stop() =>
            _sequence.Pause();

        protected void CreateSequence()
        {
            _sequence = DOTween.Sequence();

            FillSequence(_sequence);

            _sequence.OnComplete(InvokeEnded);
        }

        protected abstract void FillSequence(Sequence seq);
    }
}