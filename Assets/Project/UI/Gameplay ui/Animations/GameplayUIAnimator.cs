using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Animation
{
    [RequireComponent(typeof(GameplayUI))]
    public class GameplayUIAnimator : ShowableUIAnimator
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private TextMeshProUGUI _timeLabel;
        [SerializeField] private TextMeshProUGUI _bestTimeLabel;
        
        protected override void Awake()
        {
            base.Awake();

            var ui = new GameplayUIFader(_pauseButton, _timeLabel, _bestTimeLabel);
            _showAnimation = new GameplayUIShowAnimation(ui, _showDuration);
            _hideAnimation = new GameplayUIHideAnimation(ui, _hideDuration);
        }
    }
}