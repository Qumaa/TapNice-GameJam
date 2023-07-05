namespace Project.Game
{
    public interface IEffect<T>
    {
        T Apply(T baseValue);
        IAffectableUseCounter GetUseCounter();
    }
}