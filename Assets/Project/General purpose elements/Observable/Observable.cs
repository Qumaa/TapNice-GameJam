using System;

namespace Project
{
    public class Observable<T> : IObservable<T>
    {
        private T _value;

        public T Value
        {
            get => _value;
            set => SetValue(value);
        }

        public event Action<T, T> OnValueChanged;

        public Observable(T value)
        {
            _value = value;
        }

        private void SetValue(T value)
        {
            var old = _value;
            _value = value;
            
            OnValueChanged?.Invoke(old, _value);
        }

        public static implicit operator T(Observable<T> o) =>
            o.Value;

        public static Observable<TType> Create<TType>(TType defaultValue) =>
            new Observable<TType>(defaultValue);
    }
}