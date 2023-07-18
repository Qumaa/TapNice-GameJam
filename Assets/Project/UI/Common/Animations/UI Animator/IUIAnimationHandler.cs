using System;

namespace Project.UI.Animation
{
    public interface IUIAnimationHandler
    {
        void HandleAnimation(IUIAnimation animation, Action onEnded);
    }

    public static class UIAnimationHandlerExtensions
    {
        public static void HandleAnimation(this IUIAnimationHandler handler, IUIAnimation animation) =>
            handler.HandleAnimation(animation, null);
    }
}