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
            foreach (var effect in AllEffects())
                if (!effect.GetUseCounter().Use())
                    ExpireEffect(effect);
        }

        public void ClearEffects()
        {
            foreach (var effect in AllEffects())
                ExpireEffect(effect);
        }

        private void ExpireEffect(IEffect effect)
        {
            effect.Expire();
            _effects.Remove(effect);
        }

        private IEnumerable<IEffect> AllEffects()
        {
            for (var i = _effects.Count - 1; i >= 0; i--)
                yield return _effects[i];
        }
    }
}