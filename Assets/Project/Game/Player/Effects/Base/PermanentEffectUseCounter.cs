namespace Project.Game
{
    public class PermanentEffectUseCounter : IEffectUseCounter
    {
        public bool Use() =>
            true;

        public void Reset()
        {
        }
    }
}