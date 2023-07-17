﻿using System;

namespace Project.UI.Animation
{
    public class UniversalUIAnimationHandler : IUIAnimationHandler
    {
        private Action _currentCallback;
        private IUIAnimation _currentAnimation;
        
        public void HandleAnimation(IUIAnimation animation, Action onEnded)
        {
            TerminateCachedAnimation();
            CacheAnimationAndCallback(animation, onEnded);
            StartCachedAnimation();
        }

        private void TerminateCachedAnimation()
        {
            if (_currentAnimation == null)
                return;
            
            _currentAnimation.Stop();
            _currentAnimation.OnEnded -= _currentCallback;
        }

        private void CacheAnimationAndCallback(IUIAnimation animation, Action onEnded)
        {
            _currentAnimation = animation;
            _currentCallback = onEnded;
        }

        private void StartCachedAnimation()
        {
            _currentAnimation.OnEnded += _currentCallback;
            _currentAnimation.Play();
        }
    }
}