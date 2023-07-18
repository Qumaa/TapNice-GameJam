using Project.UI.Animation;
using TMPro;
using UnityEngine.UI;

namespace Project.UI
{
    public class GameplayUIFader : UIFader
    {
        private readonly Button _pauseButton;
        private readonly TextMeshProUGUI _timeLabel;

        public GameplayUIFader(Button pauseButton, TextMeshProUGUI timeLabel)
        {
            _pauseButton = pauseButton;
            _timeLabel = timeLabel;
        }

        protected override void SetFade(float fade)
        {
            SetButtonFade(fade);
            SetScoreFade(fade);
        }

        private void SetButtonFade(float fade)
        {
            var color = _pauseButton.image.color;
            color.a = fade;
            _pauseButton.image.color = color;
        }

        private void SetScoreFade(float fade)
        {
            var color = _timeLabel.color;
            color.a = fade;
            _timeLabel.color = color;
        }
    }
}