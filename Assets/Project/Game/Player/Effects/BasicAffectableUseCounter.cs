namespace Project.Game
{
    public struct BasicAffectableUseCounter : IAffectableUseCounter
    {
        private readonly int _defaultUses;
        private int _usesLeft;

        public BasicAffectableUseCounter(int uses)
        {
            _usesLeft = _defaultUses = uses;
        }

        public bool Use() =>
            _usesLeft-- > 0;

        public void Reset() =>
            _usesLeft = _defaultUses;
    }
}