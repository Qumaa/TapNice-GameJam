namespace Project
{
    public interface ILoadableFrom<T>
    {
        void Load(T data);
    }
}