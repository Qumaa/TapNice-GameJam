using System;

namespace Project.Game.Player
{
    public interface IGameInputService
    {
        event Action OnScreenTouchInput;
    }
}