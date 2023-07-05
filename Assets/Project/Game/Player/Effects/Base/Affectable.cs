using System.Collections.Generic;
using System.Linq;

namespace Project.Game
{
    public class Affectable<T> : IAffectable<T>
    {
        private readonly List<IEffect<T>> _effects;
        private readonly IEffectsManager _effectsManager;
        private T _rawValue;

        public T RawValue
        {
            get => _rawValue;
            set => SetRawValue(value);
        }

        public T AffectedValue { get; private set; }

        public Affectable(T rawValue, IEffectsManager effectsManager)
        {
            AffectedValue = _rawValue = rawValue;
            _effectsManager = effectsManager;
            _effects = new List<IEffect<T>>();
        }

        public void AddEffect(IEffect<T> effect)
        {
            _effects.Add(effect);
            _effectsManager.AddEffect(effect);
            effect.SetSource(this);
            CacheAffectedValue();
        }

        public void RemoveEffect(IEffect<T> effect)
        {
            _effects.Remove(effect);
            CacheAffectedValue();
        }

        private void SetRawValue(T newValue)
        {
            _rawValue = newValue;
            CacheAffectedValue();
        }

        private void CacheAffectedValue() =>
            AffectedValue = CalculateAffectedValue();

        private T CalculateAffectedValue() =>
            _effects.Aggregate(RawValue, (accumulated, effect) => effect.ApplyTo(accumulated));
    }
}