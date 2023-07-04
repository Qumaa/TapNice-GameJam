namespace Project
{
    public interface IInstanceContainer<in T>
    {
        void Add(T item);
        void Remove(T item);
    }
}