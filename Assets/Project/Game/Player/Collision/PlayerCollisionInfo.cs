using System;
using System.Linq;
using Project.Game.Player;
using UnityEngine;

namespace Project.Game.CollisionHandling
{
    public readonly struct PlayerCollisionInfo
    {
        private static readonly float _directionError = Mathf.Cos(45); // error in angles

        public readonly bool IsOnFloor;
        public readonly bool IsOnWall;
        public readonly Vector2 RawNormal;
        public readonly Vector2 NormalizedNormal;
        public readonly IPlayer Player;

        public PlayerCollisionInfo(Collision2D collision, Vector2 up,
            IPlayer player, Vector2 playerPosition)
        {
            Player = player;
                    
            RawNormal = CalculateNormal(collision.contacts, playerPosition);
            NormalizedNormal = RawNormal.normalized;

            IsOnFloor = IsInTheSameDirection(NormalizedNormal, up);
            IsOnWall = !IsOnFloor && !IsInTheSameDirection(NormalizedNormal, -up);
        }

        private static Vector2 CalculateNormal(ContactPoint2D[] contacts, Vector2 playerPos) =>
            contacts.Length switch
            {
                0 => throw new ArgumentException(),
                1 => ApproximateNormal(contacts[0], playerPos),
                _ => contacts.Aggregate(
                    ApproximateNormal(contacts[0], playerPos),
                    (accumulated, next) => (accumulated + ApproximateNormal(next, playerPos)) / 2f
                )
            };

        private static bool IsInTheSameDirection(Vector2 direction1, Vector2 direction2) =>
            (1 - Vector2.Dot(direction1, direction2)) < _directionError;

        private static Vector2 ApproximateNormal(ContactPoint2D point, Vector2 playerPos) =>
            (playerPos - point.point).normalized;
    }
}