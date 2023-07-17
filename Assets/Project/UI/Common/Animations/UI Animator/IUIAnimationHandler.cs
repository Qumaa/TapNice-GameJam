using System;

namespace Project.UI.Animation
{
    public interface IUIAnimationHandler
    {
        void HandleAnimation(IUIAnimation animation, Action onEnded);
    }
}