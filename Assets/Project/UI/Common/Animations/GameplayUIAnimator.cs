using System;
using UnityEngine;

namespace Project.UI.Animation
{
    [RequireComponent(typeof(GameplayUI))]
    public class GameplayUIAnimator : MonoBehaviour, IShowableUIAnimator
    {
        [SerializeField] private float _showDuration;
        [SerializeField] private float _hideDuration;

        private UniversalUIAnimationHandler _handler;
        private GameplayUIShowAnimation _showAnimation;
        private GameplayUIHideAnimation _hideAnimation;

        private void Awake()
        {
            _handler = new UniversalUIAnimationHandler();
            
            var ui = GetComponent<GameplayUI>();
            _showAnimation = new GameplayUIShowAnimation(ui, _showDuration);
            _hideAnimation = new GameplayUIHideAnimation(ui, _hideDuration);
        }

        public IUIAnimation PlayShowingAnimation()
        {
            _showAnimation.Play();
            return _showAnimation;
        }

        public IUIAnimation PlayHidingAnimation()
        {
            _hideAnimation.Play();
            return _hideAnimation;
        }

        public IUIAnimationHandler GetAnimationHandler() =>
            _handler;
    }
}