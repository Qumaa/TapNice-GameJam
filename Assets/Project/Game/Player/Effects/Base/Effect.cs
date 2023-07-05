namespace Project.Game
{
    public abstract class Effect<T> : IEffect<T>
    {
        private IAffectable<T> _source;
        private IEffectUseCounter _useCounter;

        public abstract T ApplyTo(T baseValue);

        void IEffect<T>.SetSource(IAffectable<T> source) =>
            _source = source;

        public void Expire()
        {
            _source.RemoveEffect(this);
            _useCounter.Reset();
        }

        public IEffectUseCounter GetUseCounter() =>
            _useCounter ??= CreateUseCounter();

        protected abstract IEffectUseCounter CreateUseCounter();
    }
}