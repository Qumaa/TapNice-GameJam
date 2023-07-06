namespace Project
{
    public interface IContainer<T>
    {
        T Resolve();
        void Pool(T item);
    }
}