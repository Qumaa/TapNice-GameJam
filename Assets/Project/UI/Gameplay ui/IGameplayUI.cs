using System;

namespace Project.UI
{
    public interface IGameplayUI : IGameUI
    {
        event Action OnPausePressed;
        
        void DisplayTime(float time);
        void SetHighestTime(float highestTime);
    }
}