using System;
using UnityEngine;

namespace Project.Game
{
    public interface IPlayer
    {
        void SetJumpingStatus(bool status);
        bool TryJump(float height);
        void ForceJump(float height);
        void InvertDirection();
        void SetDirection(PlayerDirection direction);
        event Action<PlayerCollisionInfo> OnCollided;
    }
}