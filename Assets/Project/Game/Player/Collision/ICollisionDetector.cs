using System;
using UnityEngine;

namespace Project.Game.CollisionHandling
{
    public interface ICollisionDetector : IResettable
    {
        public event Action<Collision2D> OnCollided;
    }
}