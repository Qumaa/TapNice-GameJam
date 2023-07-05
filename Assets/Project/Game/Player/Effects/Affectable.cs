using System.Collections.Generic;
using System.Linq;

namespace Project.Game
{
    public class Affectable<T> : IAffectable<T>
    {
        private List<IEffect<T>> _effects;
        private T _baseValue;

        public T BaseValue
        {
            get => _baseValue;
            set => SetBaseValue(value);
        }

        public T AffectedValue { get; private set; }

        public void Add(IEffect<T> effect)
        {
            _effects.Add(effect);
            CacheAffectedValue();
        }

        public void Remove(IEffect<T> effect)
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
            _effects.Aggregate(BaseValue, (accumulated, effect) => effect.Apply(accumulated));
    }
}