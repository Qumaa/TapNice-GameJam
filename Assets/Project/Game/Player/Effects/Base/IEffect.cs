﻿using System;

namespace Project.Game
{
    public interface IEffect
    {
        event Action<IEffect> OnExpired; 
        void Expire();
        IEffectUseCounter GetUseCounter();
        bool CanReuse();
        void Reuse();
    }
}