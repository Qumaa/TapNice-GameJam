using System;
using Project.UI.Animation;

namespace Project.UI
{
    public interface IGameplayWinUI : IShowableGameUI, IAnimatedShowableUI
    {
        event Action OnNextLevelPressed;
        event Action OnRestartPressed;
        event Action OnQuitLevelPressed;
        void SetNextLevelButtonAvailability(bool availability);
        void SetElapsedTime(float time);
        void SetHighestTime(float highestTime);
    }
}