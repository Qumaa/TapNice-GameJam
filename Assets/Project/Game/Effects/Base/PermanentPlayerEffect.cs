namespace Project.Game.Effects
{
    public abstract class PermanentPlayerEffect<T> : Effect<T>
    {
        protected sealed override IEffectUseCounter CreateUseCounter() =>
            new PermanentEffectUseCounter();
    }
}