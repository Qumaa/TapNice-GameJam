using System;
using System.Linq;
using UnityEngine;

namespace Project.Game
{
    public readonly struct PlayerCollisionInfo
    {
        private const float _ROUNDING_ERROR = 0.01f;
        
        public readonly bool IsVertical;
        public readonly bool IsHorizontal;
        public readonly Vector2 RawNormal;
        public readonly Vector2 NormalizedNormal;
        public readonly IPlayerCollisionHandler CollisionHandler;

        public PlayerCollisionInfo(Collision2D collision, Vector2 up, IPlayerCollisionHandler collisionHandler)
        {
            CollisionHandler = collisionHandler;
            
            RawNormal = CalculateNormal(collision.contacts);
            NormalizedNormal = RawNormal.normalized;

            IsVertical = IsInTheSameDirection(NormalizedNormal, up);
            IsHorizontal = !IsVertical;
        }

        private static Vector2 CalculateNormal(ContactPoint2D[] contacts) =>
            contacts.Length switch
            {
                0 => throw new ArgumentException(),
                1 => contacts[0].normal,
                _ => contacts.Aggregate(contacts[0].normal, (accumulated, next) => (accumulated + next.normal) / 2f)
            };

        private static bool IsInTheSameDirection(Vector2 direction1, Vector2 direction2) =>
            1 - Vector2.Dot(direction1, direction2) < _ROUNDING_ERROR;
    }
}