namespace Project.UI.Animation
{
    public abstract class UIFader : IFadeableUI
    {
        private float _fade;

        public float Fade
        {
            get => _fade;
            set
            {
                _fade = value;
                SetFade(_fade);
            }
        }

        protected abstract void SetFade(float fade);
    }
}