using System;
using UnityEngine;

namespace Project.Game.CollisionHandling
{
    public interface ICollisionDetector : IResettable
    {
        public event Action<CustomCollision2D> OnCollided;
    }
}