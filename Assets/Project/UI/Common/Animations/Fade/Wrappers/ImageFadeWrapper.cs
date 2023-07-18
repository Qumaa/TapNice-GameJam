using UnityEngine.UI;

namespace Project.UI.Animation
{
    public class ImageFadeWrapper : IFadeableUI
    {
        private readonly Image _wrappedImage;

        public ImageFadeWrapper(Image wrappedImage)
        {
            _wrappedImage = wrappedImage;
        }

        public float GetFade() =>
            _wrappedImage.color.a;

        public void SetFade(float fade)
        {
            var color = _wrappedImage.color;
            color.a = fade;
            _wrappedImage.color = color;
        }
    }
}