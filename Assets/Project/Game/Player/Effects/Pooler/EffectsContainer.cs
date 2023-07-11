using System;

namespace Project.Game
{
    public class EffectsContainer<T> : IContainer<T> 
        where T : IEffect
    {
        private readonly Func<T> _factory;
        private readonly Pooler<T> _pooler;

        public EffectsContainer(Func<T> factory)
        {
            _factory = factory;
            _pooler = new Pooler<T>();
        }

        public T Resolve()
        {
            if (!_pooler.TryPop(out var effect))
                effect = _factory();
            else
                effect.Reuse();
            
            effect.OnExpired += HandleExpired;
            return effect;
        }

        public void Pool(T effect) =>
            _pooler.Push(effect);

        private void HandleExpired(IEffect effect)
        {
            effect.OnExpired -= HandleExpired;
            Pool((T)effect);
        }
    }
}
