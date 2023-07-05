namespace Project.Game
{
    public class ScalePlayerEffect : UsablePlayerEffect<float>
    {
        private readonly float _scale;

        public ScalePlayerEffect(int uses, float scale) : base(uses)
        {
            _scale = scale;
        }

        public override float ApplyTo(float baseValue) =>
            baseValue * _scale;
    }
}