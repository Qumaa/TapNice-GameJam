using System;

namespace Project.UI
{
    public interface IGameplayWinUI : IShowableGameUI
    {
        event Action OnNextLevelPressed;
        event Action OnRestartPressed;
        event Action OnQuitLevelPressed;
        void SetNextLevelButtonAvailability(bool availability);
        void SetElapsedTime(float time);
        void SetHighestTime(float highestTime);
    }
}