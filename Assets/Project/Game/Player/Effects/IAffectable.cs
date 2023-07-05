namespace Project.Game
{
    public interface IAffectable<T>
    {
        T BaseValue { get; set; }
        T AffectedValue { get; }
        void Add(IEffect<T> effect);
        void Remove(IEffect<T> effect);
    }
}