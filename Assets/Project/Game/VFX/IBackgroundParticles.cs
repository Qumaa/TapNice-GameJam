using UnityEngine;

namespace Project.Game.VFX
{
    public interface IBackgroundParticles : IResettable, IActivatable, IUpdatable
    {
        void Init(Vector2 areaFrom, Vector2 areaTo);
    }
}