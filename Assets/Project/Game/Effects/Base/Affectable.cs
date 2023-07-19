using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Game.Effects
{
    public class Affectable<T> : IAffectable<T>
    {
        private readonly LinkedList<IEffect<T>> _effects;
        private readonly IEffectsManager _effectsManager;
        private T _rawValue;

        public T RawValue
        {
            get => _rawValue;
            set => SetRawValue(value);
        }

        public T AffectedValue { get; private set; }
        public event Action<T> OnAffectedValueChanged;

        public Affectable(T rawValue, IEffectsManager effectsManager)
        {
            AffectedValue = _rawValue = rawValue;
            _effectsManager = effectsManager;
            _effects = new LinkedList<IEffect<T>>();
        }

        public void AddEffect(IEffect<T> effect)
        {
            _effects.AddLast(effect);
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

        private void CacheAffectedValue()
        {
            AffectedValue = CalculateAffectedValue();
            OnAffectedValueChanged?.Invoke(AffectedValue);
        }

        private T CalculateAffectedValue() =>
            _effects.Aggregate(RawValue, (accumulated, effect) => effect.ApplyTo(accumulated));
    }
}