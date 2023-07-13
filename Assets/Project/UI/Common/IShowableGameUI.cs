namespace Project.UI
{
    public interface IShowableGameUI : IGameUI, IShowableUI { }

    public static class ShowableUIExtensions
    {
        public static T ShowFluent<T>(this T ui) where T : IShowableUI
        {
            ui.Show();
            return ui;
        }

        public static T HideFluent<T>(this T ui) where T : IShowableUI
        {
            ui.Hide();
            return ui;
        }
    }
}