namespace Project.Game
{
    public abstract class Effect<T> : IEffect<T>
    {
        private IAffectableUseCounter _useCounter;
        
        public abstract T Apply(T baseValue);

        public IAffectableUseCounter GetUseCounter() =>
            _useCounter ??= CreateUseCounter();

        protected abstract IAffectableUseCounter CreateUseCounter();
    }
}