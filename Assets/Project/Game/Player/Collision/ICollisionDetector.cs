using System;
using UnityEngine;

namespace Project.Game
{
    public interface ICollisionDetector
    {
        public event Action<Collision2D> OnCollided;
    }
}