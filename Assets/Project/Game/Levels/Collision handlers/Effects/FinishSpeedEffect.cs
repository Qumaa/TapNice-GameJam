namespace Project.Game.Effects
{
    public class FinishSpeedEffect : PermanentPlayerEffect<float>
    {
        public override float ApplyTo(float baseValue) =>
            baseValue * 0;
    }
}