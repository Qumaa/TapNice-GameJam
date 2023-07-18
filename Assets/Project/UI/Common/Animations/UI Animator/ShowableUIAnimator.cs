using UnityEngine;

namespace Project.UI.Animation
{
    public abstract class ShowableUIAnimator : MonoBehaviour, IShowableUIAnimator
    {
        [SerializeField] protected float _showDuration;
        [SerializeField] protected float _hideDuration;

        private UniversalUIAnimationHandler _handler;
        protected IUIAnimation _showAnimation;
        protected IUIAnimation _hideAnimation;

        protected virtual void Awake()
        {
            _handler = new UniversalUIAnimationHandler();
        }
        
        public IUIAnimation PlayShowingAnimation()
        {
            GetAnimationHandler().HandleAnimation(_showAnimation);
            return _showAnimation;
        }

        public IUIAnimation PlayHidingAnimation()
        {
            GetAnimationHandler().HandleAnimation(_hideAnimation);
            return _hideAnimation;
        }

        public IUIAnimationHandler GetAnimationHandler() =>
            _handler;
    }
}