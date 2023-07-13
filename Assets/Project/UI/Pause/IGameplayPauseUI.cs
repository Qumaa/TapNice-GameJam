using System;

namespace Project.UI
{
    public interface IGameplayPauseUI : IShowableGameUI
    {
        event Action OnResumePressed;
        event Action OnRestartPressed;
        event Action OnQuitLevelPressed;
    }
}