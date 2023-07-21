namespace Project.Game.Effects
{
    public class PermanentEffectUseCounter : IEffectUseCounter
    {
        public bool CanUse() =>
            true;

        public bool Use() =>
            true;

        public void Reset() { }

        public void MarkAsUnusable() { }
    }
}