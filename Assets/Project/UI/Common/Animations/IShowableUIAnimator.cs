
using System;

namespace Project.UI.Animation
{
    public interface IShowableUIAnimator
    {
        IUIAnimation PlayShowingAnimation();
        IUIAnimation PlayHidingAnimation();
        IUIAnimationHandler GetAnimationHandler();
    }

    public static class ShowableUIAnimatorExtensions
    {
        public static void PlayShowingAnimationHandled(this IShowableUIAnimator animator, Action onEnded) =>
            HandleAnimation(animator, animator.PlayShowingAnimation(), onEnded);
        
        public static void PlayHidingAnimationHandled(this IShowableUIAnimator animator, Action onEnded) =>
            HandleAnimation(animator, animator.PlayHidingAnimation(), onEnded);

        public static void HandleAnimation(this IShowableUIAnimator animator, IUIAnimation animation, Action onEnded) =>
            animator.GetAnimationHandler().HandleAnimation(animation, onEnded);
    }
}