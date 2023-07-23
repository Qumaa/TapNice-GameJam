namespace Project.Game.Effects
{
    public interface IEffectsManager
    {
        void AddEffect(IEffect effect);
        void UseEffects();
        void ClearEffects();
    }
}