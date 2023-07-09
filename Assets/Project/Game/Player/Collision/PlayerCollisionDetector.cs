using System;
using UnityEngine;

namespace Project.Game
{
    public class PlayerCollisionDetector : MonoBehaviour, ICollisionDetector
    {
        private bool _hasCollided;
        public event Action<Collision2D> OnCollided;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_hasCollided)
                return;

            _hasCollided = true;
            
            OnCollided?.Invoke(other);
        }

        private void OnCollisionExit2D() =>
            Reset();

        public void Reset() =>
            _hasCollided = false;
    }
}