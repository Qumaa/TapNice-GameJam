using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public interface IPlayerTrail : IResettable, IActivatable
    {
        void SetColor(Color color);
        void SetLifetime(float lifetime);
    }
}