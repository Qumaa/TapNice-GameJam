using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Game.CollisionHandling
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerCollisionDetector : MonoBehaviour, ICollisionDetector
    {
        private Rigidbody2D _rigidbody;
        private readonly List<ContactPoint2D> _points = new(4);

        public event Action<Collision2D> OnCollided;

        private void Awake() =>
            _rigidbody = GetComponent<Rigidbody2D>();

        private void OnCollisionEnter2D(Collision2D other)
        {
            AddPoints(other);
            OnCollided?.Invoke(other);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (TryAddPoints(other))
                OnCollided?.Invoke(other);
        }

        private bool TryAddPoints(Collision2D other)
        {
            var has = true;

            foreach (var point in other.contacts)
                if (IsCloseEnoughToAnyCachedPoints(point))
                    has = false;
                else
                    _points.Add(point);

            return has;
        }

        private void AddPoints(Collision2D other) =>
            _points.AddRange(other.contacts);

        private bool IsCloseEnoughToAnyCachedPoints(ContactPoint2D point) =>
            _points.Any(cached => Vector2.Distance(cached.point, point.point) < CalculateContactPointsDistance());

        private float CalculateContactPointsDistance() =>
            (_rigidbody.velocity * Time.fixedDeltaTime).magnitude;

        private void OnCollisionExit2D() =>
            Reset();

        public void Reset() =>
            _points.Clear();
    }
}