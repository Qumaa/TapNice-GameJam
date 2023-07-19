using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Game.CollisionHandling
{
    public class PlayerCollisionDetector : MonoBehaviour, ICollisionDetector
    {
        private readonly List<ContactPoint2D> _points = new(4);

        public event Action<Collision2D> OnCollided;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            TryAddPoints(other);
            OnCollided?.Invoke(other);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (TryAddPoints(other))
                OnCollided?.Invoke(other);
        }

        private bool TryAddPoints(Collision2D other)
        {
            var alreadyHasGivenPoints = true;

            foreach (var point in other.contacts)
                if (IsCloseEnoughToAnyCachedPoints(point, CalculateContactPointsDistance(other)))
                    alreadyHasGivenPoints = false;
                else
                    _points.Add(point);

            return alreadyHasGivenPoints;
        }

        private void AddPoints(Collision2D other) =>
            _points.AddRange(other.contacts);

        private bool IsCloseEnoughToAnyCachedPoints(ContactPoint2D point, float minDistance) =>
            _points.Any(cached => Vector2.Distance(cached.point, point.point) < minDistance);

        private static float CalculateContactPointsDistance(Collision2D collision2D) =>
            collision2D.otherRigidbody.velocity.magnitude * Time.fixedDeltaTime;

        private void OnCollisionExit2D() =>
            Reset();

        public void Reset() =>
            _points.Clear();
    }
}