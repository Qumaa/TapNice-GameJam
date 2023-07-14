using System.Collections;
using System.Collections.Generic;

namespace Project
{
    public class Pooler<TTarget> : IPooler<TTarget>, IEnumerable<TTarget>
    {
        private readonly Stack<TTarget> _pool = new();

        public void Push(TTarget objToPool) =>
            _pool.Push(objToPool);

        public bool CanGet() =>
            _pool.TryPeek(out _);

        public TTarget Get() =>
            _pool.Pop();

        public IEnumerator<TTarget> GetEnumerator() =>
            _pool.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}