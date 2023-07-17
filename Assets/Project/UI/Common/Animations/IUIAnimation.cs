using System;

namespace Project.UI.Animation
{
    public interface IUIAnimation
    {
        event Action OnEnded;
        void Play();
        void Stop();
    }
}
