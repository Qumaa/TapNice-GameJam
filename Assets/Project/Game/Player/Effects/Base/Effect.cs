using System;

namespace Project.Game
{
    public abstract class Effect<T> : IEffect<T>
    {
        private IAffectable<T> _source;
        private IEffectUseCounter _useCounter;

        public event Action<IEffect> OnExpired;
        public abstract T ApplyTo(T baseValue);

        void IEffect<T>.SetSource(IAffectable<T> source) =>
            _source = source;

        public void Expire()
        {
            _source.RemoveEffect(this);
            GetUseCounter().MarkAsUnusable();
            OnExpired?.Invoke(this);
        }

        public IEffectUseCounter GetUseCounter() =>
            _useCounter ??= CreateUseCounter();
        
        public void Reuse() =>
            GetUseCounter().Reset();

        protected abstract IEffectUseCounter CreateUseCounter();
    }
}