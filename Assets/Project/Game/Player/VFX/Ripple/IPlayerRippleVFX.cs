using UnityEngine;

namespace Project.Game.Player.VFX
{
    public interface IPlayerRippleVFX : IResettable
    {
        void PlayRipple(Vector2 position, Color color);
    }
}
