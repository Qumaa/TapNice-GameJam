namespace Project.UI
{
    public abstract class ShowableGameUI : GameUI, IShowableGameUI
    {
        public virtual void Show() =>
            _transform.gameObject.SetActive(true);

        public virtual void Hide() =>
            _transform.gameObject.SetActive(false);
    }
}