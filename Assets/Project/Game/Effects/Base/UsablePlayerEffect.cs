namespace Project.Game.Effects
{
    public abstract class UsablePlayerEffect<T> : Effect<T>
    {
        private readonly int _uses;

        protected UsablePlayerEffect(int uses)
        {
            _uses = uses;
        }

        protected sealed override IEffectUseCounter CreateUseCounter() =>
            new BasicEffectUseCounter(_uses);
    }
}