using System;

namespace Project.UI.Animation
{
    public abstract class FadeUIAnimation : IUIAnimation
    {
        private readonly IFadeableUI _ui;

        public event Action OnEnded;

        protected FadeUIAnimation(IFadeableUI ui)
        {
            _ui = ui;
        }

        public abstract void Play();

        public abstract void Stop();

        protected void SetUIFade(float fade) =>
            _ui.Fade = fade;

        protected float GetUIFade() =>
            _ui.Fade;
        
        protected void InvokeEnded() =>
            OnEnded?.Invoke();
    }
}