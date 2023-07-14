using System;
using System.Collections.Generic;

namespace Project
{
    public class Container<T> : IContainer<T>
    {
        private readonly List<T> _active;
        private readonly Func<T> _factory;
        private readonly Pooler<T> _pool;

        public Container(Func<T> factory)
        {
            _factory = factory;
            _pool = new Pooler<T>();
            _active = new List<T>();
        }

        public Container(IFactory<T> factory) : this(factory.CreateNew) { }

        public IEnumerable<T> Active => _active;
        public IEnumerable<T> Pooled => _pool;
        public IEnumerable<T> All => LoopThroughAll();

        public event Action<T> OnItemCreated;
        public event Action<T> OnItemReleased;
        public event Action<T> OnItemPooled;
        public event Action<T> OnItemResolved;

        public virtual T Resolve()
        {
            if (!_pool.TryGet(out var item))
            {
                item = _factory();
                OnItemCreated?.Invoke(item);
            }
            else
                OnItemReleased?.Invoke(item);

            _active.Add(item);
            OnItemResolved?.Invoke(item);
            return item;
        }

        public virtual void Pool(T item)
        {
            _pool.Push(item);
            _active.Remove(item);
            OnItemPooled?.Invoke(item);
        }

        private IEnumerable<T> LoopThroughAll()
        {
            foreach (var active in _active)
                yield return active;

            foreach (var pooled in _pool)
                yield return pooled;
        }
    }
}