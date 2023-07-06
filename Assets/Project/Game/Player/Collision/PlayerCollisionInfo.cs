using System;
using System.Linq;
using UnityEngine;

namespace Project.Game
{
    public readonly struct PlayerCollisionInfo
    {
        private static readonly float _directionError = Mathf.Cos(45); // error in angles

        public readonly bool IsDirectedUp;
        public readonly bool IsHorizontal;
        public readonly Vector2 RawNormal;
        public readonly Vector2 NormalizedNormal;
        public readonly IPlayer Player;
        public readonly ICollisionHandler CollisionHandler;

        public PlayerCollisionInfo(Collision2D collision, Vector2 up, ICollisionHandler collisionHandler, IPlayer player)
        {
            CollisionHandler = collisionHandler;
            Player = player;

            RawNormal = CalculateNormal(collision.contacts);
            NormalizedNormal = RawNormal.normalized;

            IsDirectedUp = IsInTheSameDirection(NormalizedNormal, up);
            IsHorizontal = !IsDirectedUp && !IsInTheSameDirection(NormalizedNormal, -up);
        }

        private static Vector2 CalculateNormal(ContactPoint2D[] contacts) =>
            contacts.Length switch
            {
                0 => throw new ArgumentException(),
                1 => contacts[0].normal,
                _ => contacts.Aggregate(contacts[0].normal, (accumulated, next) => (accumulated + next.normal) / 2f)
            };

        private static bool IsInTheSameDirection(Vector2 direction1, Vector2 direction2) =>
            1 - Vector2.Dot(direction1, direction2) < _directionError;
    }
}