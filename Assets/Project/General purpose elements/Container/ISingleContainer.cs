namespace Project
{
    public interface ISingleContainer<in TItem>
    {
        void Add<T>(T item) where T : TItem;
        T Get<T>() where T : TItem;
    }
}