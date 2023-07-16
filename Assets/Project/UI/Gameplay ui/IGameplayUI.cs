using System;

namespace Project.UI
{
    public interface IGameplayUI : IShowableGameUI
    {
        event Action OnPausePressed;
        
        void DisplayTime(float time);
        void SetHighestTime(float highestTime);
    }
}