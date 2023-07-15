namespace Project.Game.Effects
{
    public interface IAffectable<T>
    {
        T RawValue { get; set; }
        T AffectedValue { get; }
        void AddEffect(IEffect<T> effect);
        void RemoveEffect(IEffect<T> effect);
    }
}