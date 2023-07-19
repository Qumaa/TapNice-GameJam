using System;

namespace Project.UI.Animation
{
    public abstract class ShowableUIAnimation : IUIAnimation
    {
        public event Action OnEnded;

        public abstract void Play();

        public abstract void Stop();
        public abstract void Delete();

        protected void InvokeEnded() =>
            OnEnded?.Invoke();
    }
}