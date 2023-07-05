namespace Project.Game
{
    public interface IEffect<T> : IEffect
    {
        T ApplyTo(T baseValue);
        void SetSource(IAffectable<T> source);
    }
}