using System.Collections.Generic;

namespace Project.Game
{
    public class EffectsManager : IEffectsManager
    {
        private readonly List<IEffect> _effects = new();

        public void AddEffect(IEffect effect)
        {
            _effects.Add(effect);
        }

        public void UseEffects()
        {
            for (var i = _effects.Count - 1; i >= 0; i--)
            {
                var effect = _effects[i];
                
                if (effect.GetUseCounter().Use())
                    continue;
                
                effect.Expire();
                _effects.Remove(effect);
            }
        }
    }
}