namespace Project.Game
{
    public interface IEffectsManager
    {
        void AddEffect(IEffect effect);
        void UseEffects();
        void ClearEffects();
    }
}