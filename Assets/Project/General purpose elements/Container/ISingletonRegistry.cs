namespace Project
{
    public interface ISingletonRegistry<in TItem>
    {
        void Add<T>(T item) where T : TItem;
        T Get<T>() where T : TItem;
        void Remove<T>() where T : TItem;
    }

    public static class SingletonRegistryExtensions
    {
        // yes, this extension exists to simply pick up a type parameter from the passed instance
        public static void Remove<T, TItem>(this ISingletonRegistry<TItem> container, T item) where T : TItem =>
            container.Remove<T>();
    }
}