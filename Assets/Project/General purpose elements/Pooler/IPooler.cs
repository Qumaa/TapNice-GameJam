namespace Project
{
    public interface IPooler<TTarget>
    {
        void Push(TTarget objToPool);
        bool CanGet();
        TTarget Get();
    }

    public static class PoolerExtensions
    {
        public static bool TryGet<T>(this IPooler<T> pooler, out T obj)
        {
            if (pooler.CanGet())
            {
                obj = pooler.Get();
                return true;
            }

            obj = default;
            return false;
        }
    }
}