using UnityEngine;

namespace Project.UI.Animation
{
    [RequireComponent(typeof(GameplayUI))]
    public class GameplayUIAnimator : ShowableUIAnimator
    {
        protected override void Awake()
        {
            base.Awake();
            
            var ui = GetComponent<GameplayUI>();
            _showAnimation = new GameplayUIShowAnimation(ui, _showDuration);
            _hideAnimation = new GameplayUIHideAnimation(ui, _hideDuration);
        }
    }
}