﻿using System;
using UnityEngine;

namespace Project.Game.Levels
{
    public interface ILevel : IResettable
    {
        float TimeElapsed { get; }
        
        event Action OnStarted;
        event Action OnRestarted;

        /// <summary>
        /// Float argument is <see cref="TimeElapsed"/>.
        /// </summary>
        event Action<float> OnFinishedWithTime;

        event Action OnFinished;

        /// <summary>
        /// Call this method when a scene is loaded and level needs to be initialized.
        /// </summary>
        void Start(string levelName);

        void Restart();
        void Finish();
        void StopCountingTime();
    }
}