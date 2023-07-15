namespace Project.Game.Effects
{
    public interface IEffect<T> : IEffect
    {
        T ApplyTo(T baseValue);
        void SetSource(IAffectable<T> source);
    }
}