using System.Collections.Generic;

namespace Project
{
    public class Pooler<TTarget> : IPooler<TTarget>
    {
        private readonly Stack<TTarget> _pool = new();

        public void Push(TTarget objToPool) =>
            _pool.Push(objToPool);

        public bool CanPop() =>
            _pool.TryPeek(out _);

        public TTarget Pop() =>
            _pool.Pop();
    }
}