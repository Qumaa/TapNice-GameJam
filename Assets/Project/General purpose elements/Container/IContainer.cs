using System;
using System.Collections.Generic;

namespace Project
{
    public interface IContainer<T>
    {
        IEnumerable<T> Active { get; }
        IEnumerable<T> Pooled { get; }
        IEnumerable<T> All { get; }
        event Action<T> OnItemCreated;
        event Action<T> OnItemReleased;
        event Action<T> OnItemPooled;
        T Resolve();
        void Pool(T item);
    }
}