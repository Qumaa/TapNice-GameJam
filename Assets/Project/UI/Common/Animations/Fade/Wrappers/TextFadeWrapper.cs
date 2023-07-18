using TMPro;

namespace Project.UI.Animation
{
    public class TextFadeWrapper : IFadeableUI
    {
        private readonly TextMeshProUGUI _wrappedText;

        public TextFadeWrapper(TextMeshProUGUI wrappedText)
        {
            _wrappedText = wrappedText;
        }

        public float GetFade() =>
            _wrappedText.color.a;

        public void SetFade(float fade)
        {
            var color = _wrappedText.color;
            color.a = fade;
            _wrappedText.color = color;
        }
    }
}