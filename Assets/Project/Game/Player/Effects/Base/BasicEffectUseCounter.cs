namespace Project.Game
{
    public class BasicEffectUseCounter : IEffectUseCounter
    {
        private readonly int _defaultUses;
        private int _usesLeft;

        public BasicEffectUseCounter(int uses)
        {
            _usesLeft = _defaultUses = uses;
        }

        public bool CanUse() =>
            CanUseInternal(_usesLeft);

        public bool Use() =>
            CanUseInternal(_usesLeft--);

        public void Reset() =>
            _usesLeft = _defaultUses;

        public void MarkAsUnusable() =>
            _usesLeft = 0;

        private static bool CanUseInternal(int uses) =>
            uses > 0;
    }
}