using UnityEngine;

namespace Project.Game.Player
{
    public interface IPlayerTrail : IResettable, IActivatable
    {
        void SetColor(Color color);
    }
}