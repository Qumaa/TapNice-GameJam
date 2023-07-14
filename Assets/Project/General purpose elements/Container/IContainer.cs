using System;
using System.Collections.Generic;
using System.Linq;

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
        event Action<T> OnItemResolved;
        T Resolve();
        void Pool(T item);
    }

    public static class ContainerExtensions
    {
        public static void PoolRange<T>(this IContainer<T> container, IEnumerable<T> range)
        {
            foreach (var item in range)
                container.Pool(item);
        }

        public static void PoolActive<T>(this IContainer<T> container) =>
            container.PoolRange(container.Active.Reverse());

        public static void PoolActiveExcept<T>(this IContainer<T> container, params T[] excluded) =>
            container.PoolRange(container.Active.Except(excluded).Reverse());
    }
}