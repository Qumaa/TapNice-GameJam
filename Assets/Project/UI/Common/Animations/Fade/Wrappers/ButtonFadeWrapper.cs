using UnityEngine.UI;

namespace Project.UI.Animation
{
    public class ButtonFadeWrapper : IFadeableUI
    {
        private readonly ImageFadeWrapper _wrappedButtonImage;

        public ButtonFadeWrapper(Button wrappedButton)
        {
            _wrappedButtonImage = new ImageFadeWrapper(wrappedButton.image);
        }

        public float GetFade() =>
            _wrappedButtonImage.GetFade();

        public void SetFade(float fade) =>
            _wrappedButtonImage.SetFade(fade);
    }
}