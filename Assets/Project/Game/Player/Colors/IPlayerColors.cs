using Project.Game.Effects;
using UnityEngine;

namespace Project.Game.Player
{
    public interface IPlayerColors : IActivatable
    {
        IAffectable<Color> Color { get; }
        void UpdateColors(bool canJump);
    }
}