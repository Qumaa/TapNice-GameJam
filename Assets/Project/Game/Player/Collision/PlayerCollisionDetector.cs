using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Game.CollisionHandling
{
    public class PlayerCollisionDetector : MonoBehaviour, ICollisionDetector
    {
        private readonly List<ContactPoint2D> _points = new(4);

        public event Action<CustomCollision2D> OnCollided;

        private void OnCollisionEnter2D(Collision2D other) =>
            TryAddPoints(other);

        private void OnCollisionStay2D(Collision2D other) =>
            TryAddPoints(other);

        private void TryAddPoints(Collision2D other)
        {
            var minDist = CalculateContactPointsDistance(other);
            
            foreach (var point in other.contacts)
            {
                if (IsCloseEnoughToAnyCachedPoints(point, minDist))
                    continue;
                
                _points.Add(point);
                OnCollided?.Invoke(new CustomCollision2D(point, other.gameObject));
            }
        }

        private bool IsCloseEnoughToAnyCachedPoints(ContactPoint2D point, float minDistance) =>
            _points.Any(cached => Vector2.Distance(cached.point, point.point) < minDistance);

        private static float CalculateContactPointsDistance(Collision2D collision2D) =>
            collision2D.otherRigidbody.velocity.magnitude * Time.fixedDeltaTime * 2;

        private void OnCollisionExit2D() =>
            Reset();

        public void Reset() =>
            _points.Clear();
    }
}