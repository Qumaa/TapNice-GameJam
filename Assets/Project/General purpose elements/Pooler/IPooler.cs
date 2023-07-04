namespace Project
{
    public interface IPooler<TTarget>
    {
        void Push(TTarget objToPool);
        bool CanPop();
        TTarget Pop();
    }
}