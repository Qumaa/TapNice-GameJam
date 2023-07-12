using System;

namespace Project.UI
{
    public interface IGameplayPauseUI : IGameUI
    {
        event Action OnResumePressed;
        event Action OnRestartPressed;
        event Action OnQuitLevelPressed;
        
        void Show();
        void Hide();
    }
}