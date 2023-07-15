using System;

namespace Project.Game.Effects
{
    public interface IEffect
    {
        event Action<IEffect> OnExpired; 
        void Expire();
        IEffectUseCounter GetUseCounter();
        void Reuse();
    }
}