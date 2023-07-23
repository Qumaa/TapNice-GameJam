using System;
using Project.UI.Animation;

namespace Project.UI
{
    public interface IGameplayUI : IShowableGameUI, IAnimatedShowableUI
    {
        event Action OnPausePressed;
        
        void DisplayTime(float time);
        void SetBestTime(float bestTime);
        void HideBestTime();
    }
}