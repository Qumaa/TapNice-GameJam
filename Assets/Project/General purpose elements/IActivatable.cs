namespace Project
{
    public interface IActivatable
    {
        /// <summary>
        /// This method is called when object is not needed. Disables its any presence on the scene.
        /// </summary>
        void Deactivate();

        /// <summary>
        /// This method is called when object is need to be used. Redoes all actions made in <see cref="Deactivate"/>.
        /// </summary>
        void Activate();
    }
}