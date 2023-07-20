using UnityEngine;

namespace Project.Game.Player.VFX
{
    public interface IPlayerTrail : IResettable, IActivatable
    {
        void SetColor(Color color);
    }
}