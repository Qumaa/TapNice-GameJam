namespace Project
{
    public interface ISingleContainer<in TItem>
    {
        void Add<T>(T item) where T : TItem;
        T Get<T>() where T : TItem;
        void Remove<T>() where T : TItem;
    }

    public static class SingleContainerExtensions
    {
        // yes, this extension exists to simple pick up a generic parameter from the passed instance
        public static void Remove<T, TItem>(this ISingleContainer<TItem> container, T item) where T : TItem =>
            container.Remove<T>();
    }
}