namespace Project.UI.Animation
{
    public interface IAnimatedShowableUI : IShowableUI
    {
        void ShowAnimated();
        void HideAnimated();
    }

    public static class AnimatedShowableUIExtensions
    {
        public static T ShowAnimatedFluent<T>(this T ui) where T : IAnimatedShowableUI
        {
            ui.ShowAnimated();
            return ui;
        }

        public static T HideAnimatedFluent<T>(this T ui) where T : IAnimatedShowableUI
        {
            ui.HideAnimated();
            return ui;
        }
    }
}