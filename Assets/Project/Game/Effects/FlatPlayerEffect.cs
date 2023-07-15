namespace Project.Game.Effects
{
    public class FlatPlayerEffect : UsablePlayerEffect<float>
    {
        private readonly float _flatModifier;

        public FlatPlayerEffect(int uses, float flatModifier) : base(uses)
        {
            _flatModifier = flatModifier;
        }

        public override float ApplyTo(float baseValue) =>
            baseValue + _flatModifier;
    }
}