namespace Project.Game.Effects
{
    public class PermanentEffectUseCounter : IEffectUseCounter
    {
        private bool _usable = true;
        
        public bool CanUse() =>
            _usable;

        public bool Use() =>
            _usable;

        public void Reset() =>
            _usable = true;

        public void MarkAsUnusable() =>
            _usable = false;
    }
}