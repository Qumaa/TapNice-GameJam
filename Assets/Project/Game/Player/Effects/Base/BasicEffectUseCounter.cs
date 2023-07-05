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

        public bool Use() =>
            _usesLeft-- > 0;

        public void Reset() =>
            _usesLeft = _defaultUses;
    }
}