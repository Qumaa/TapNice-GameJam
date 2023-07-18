using UnityEngine;

namespace Project.UI.Animation
{
    [RequireComponent(typeof(GameplayWinUI))]
    public class WinUIAnimator : ShowableUIAnimator
    {
        protected override void Awake()
        {
            base.Awake();

            var ui = GetComponent<GameplayWinUI>();
            _showAnimation = new WinUIShowAnimation(ui, _showDuration);
            _hideAnimation = new WinUIHideAnimation(ui, _hideDuration);
        }
    }
}