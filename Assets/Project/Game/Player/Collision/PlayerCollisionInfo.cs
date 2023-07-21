using System.Runtime.InteropServices;
using Project.Game.Player;
using UnityEngine;

namespace Project.Game.CollisionHandling
{
    [StructLayout(LayoutKind.Auto)]
    public readonly struct PlayerCollisionInfo
    {
        private static readonly float _directionError = Mathf.Cos(45); // error in angles

        public readonly bool IsOnFloor;
        public readonly bool IsOnCeiling;
        public readonly bool IsOnWall;
        public readonly Vector2 Normal;
        public readonly IPlayer Player;

        public PlayerCollisionInfo(ContactPoint2D contact, IPlayer player)
        {
            Player = player;

            Normal = contact.normal;

            var up = Vector2.up;
            IsOnFloor = IsInTheSameDirection(Normal, up);
            IsOnCeiling = IsInTheSameDirection(Normal, -up);
            IsOnWall = !IsOnFloor && !IsOnCeiling;
        }

        private static bool IsInTheSameDirection(Vector2 direction1, Vector2 direction2) =>
            (1 - Vector2.Dot(direction1, direction2)) < _directionError;
    }
}