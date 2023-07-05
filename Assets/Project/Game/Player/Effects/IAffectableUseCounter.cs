namespace Project.Game
{
    public interface IAffectableUseCounter
    {
        /// <summary>
        /// Consumes one usage from the underlying effect
        /// </summary>
        /// <returns>true when can be used further, false otherwise</returns>
        bool Use();

        void Reset();
    }
}