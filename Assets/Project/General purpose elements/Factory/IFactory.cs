namespace Project
{
    public interface IFactory<T>
    {
        T CreateNew();
    }
}