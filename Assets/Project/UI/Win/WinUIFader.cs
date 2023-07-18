using Project.UI.Animation;
using TMPro;

namespace Project.UI
{
    public class WinUIFader : UIFader
    {
        private readonly TextMeshProUGUI _scoreText;

        public WinUIFader(TextMeshProUGUI scoreText)
        {
            _scoreText = scoreText;
        }

        protected override void SetFade(float fade)
        {
            var color = _scoreText.color;
            color.a = fade;
            _scoreText.color = color;
        }
    }
}