using System;

namespace Project.Game
{
    public class EffectsContainer<T> : Container<T> 
        where T : IEffect
    {
        public EffectsContainer(Func<T> factory) : base(factory)
        {
        }

        public override T Resolve()
        {
            var effect = base.Resolve();
            
            effect.OnExpired += HandleExpired;
            return effect;
        }

        private void HandleExpired(IEffect effect)
        {
            effect.OnExpired -= HandleExpired;
            Pool((T)effect);
        } 
    }
}
