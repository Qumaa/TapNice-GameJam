namespace Project.UI.Animation
{
    public abstract class UIFader : IFadeableUI
    {
        private float _fade;

        public float GetFade() =>
            _fade;

        public virtual void SetFade(float fade) =>
            _fade = fade;
    }
}