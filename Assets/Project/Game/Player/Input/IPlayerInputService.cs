using System;

namespace Project.Game
{
    public interface IPlayerInputService
    {
        event Action OnJumpInput;
    }
}