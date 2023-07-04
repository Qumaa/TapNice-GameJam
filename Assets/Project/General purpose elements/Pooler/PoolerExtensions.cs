namespace Project
{
    public static class PoolerExtensions
    {
        public static bool TryPop<T>(this IPooler<T> pooler, out T obj)
        {
            if (pooler.CanPop())
            {
                obj = pooler.Pop();
                return true;
            }

            obj = default;
            return false;
        }
    }
}