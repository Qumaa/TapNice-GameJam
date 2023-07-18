using System;

namespace Project.UI.Animation
{
    public abstract class ShowableUIAnimation : IUIAnimation
    {
        public event Action OnEnded;

        public abstract void Play();

        public abstract void Stop();

        protected void InvokeEnded() =>
            OnEnded?.Invoke();
    }
}