namespace Project.Game
{
    public interface IEffect
    {
        void Expire();
        IEffectUseCounter GetUseCounter();
    }
}