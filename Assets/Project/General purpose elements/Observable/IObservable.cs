using System;

namespace Project
{
    public interface IObservable<T>
    {
        T Value { get; set; }
        event Action<T, T> OnValueChanged;
    }
}