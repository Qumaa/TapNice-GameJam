using UnityEngine;

namespace Project.Game
{
    public interface IPlayerTrail : IResettable, IActivatable
    {
        void SetColor(Color color);
    }
}