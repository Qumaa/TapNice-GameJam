namespace Project
{
    public interface ITransientRegistry<in T>
    {
        void Add(T item);
        void Remove(T item);
    }
}