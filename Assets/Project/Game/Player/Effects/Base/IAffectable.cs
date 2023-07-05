namespace Project.Game
{
    public interface IAffectable<T>
    {
        T BaseValue { get; set; }
        T AffectedValue { get; }
        void AddEffect(IEffect<T> effect);
        void RemoveEffect(IEffect<T> effect);
    }
}