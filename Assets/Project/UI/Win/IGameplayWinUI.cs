using System;

namespace Project.UI
{
    public interface IGameplayWinUI : IShowableGameUI
    {
        event Action OnNextLevelPressed;
        event Action OnRestartPressed;
        event Action OnQuitLevelPressed;
        public void SetNextLevelButtonAvailability(bool availability);
    }
}