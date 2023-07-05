using System.Collections.Generic;
using System.Linq;

namespace Project.Game
{
    public class Affectable<T> : IAffectable<T>
    {
        private readonly List<IEffect<T>> _effects;
        private T _baseValue;

        public T BaseValue
        {
            get => _baseValue;
            set => SetBaseValue(value);
        }

        public T AffectedValue { get; private set; }

        public Affectable(T baseValue)
        {
            _baseValue = baseValue;
            _effects = new List<IEffect<T>>();
        }

        public void AddEffect(IEffect<T> effect)
        {
            _effects.Add(effect);
            effect.SetSource(this);
            CacheAffectedValue();
        }

        public void RemoveEffect(IEffect<T> effect)
        {
            _effects.Remove(effect);
            CacheAffectedValue();
        }

        private void SetBaseValue(T newValue)
        {
            _baseValue = newValue;
            CacheAffectedValue();
        }

        private void CacheAffectedValue() =>
            AffectedValue = CalculateAffectedValue();

        private T CalculateAffectedValue() =>
            _effects.Aggregate(BaseValue, (accumulated, effect) => effect.ApplyTo(accumulated));

        public static implicit operator Affectable<T>(T val) =>
            new(val);

        public static implicit operator T(Affectable<T> affectable) =>
            affectable.AffectedValue;
    }
}