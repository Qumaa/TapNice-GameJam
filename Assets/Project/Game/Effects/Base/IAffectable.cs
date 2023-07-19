using System;

namespace Project.Game.Effects
{
    public interface IAffectable<T>
    {
        T RawValue { get; set; }
        T AffectedValue { get; }
        event Action<T> OnAffectedValueChanged;
        void AddEffect(IEffect<T> effect);
        void RemoveEffect(IEffect<T> effect);
    }
}