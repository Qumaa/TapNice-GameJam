using System.Collections.Generic;

namespace Project
{
    public class Pooler<TTarget> : IPooler<TTarget>
    {
        private Stack<TTarget> _pool;

        public Pooler()
        {
            _pool = new Stack<TTarget>();
        }

        public void Push(TTarget objToPool) =>
            _pool.Push(objToPool);

        public bool CanPop() =>
            _pool.TryPeek(out _);

        public TTarget Pop() =>
            _pool.Pop();
    }
}