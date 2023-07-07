using System;

namespace Project.Game
{
    public interface IGameInputService
    {
        event Action OnScreenTouchInput;
    }
}