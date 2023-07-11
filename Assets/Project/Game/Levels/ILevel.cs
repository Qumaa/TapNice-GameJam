using System;
using UnityEngine;

namespace Project.Game
{
    public interface ILevel
    {
        IObservable<Vector2> Gravity { get; }
        float TimeElapsed { get; }
        /// <summary>
        /// Float argument is <see cref="TimeElapsed"/>.
        /// </summary>
        public event Action<float> OnFinishedWithTime;

        event Action OnFinished;

        /// <summary>
        /// Call this method when a scene is loaded and level needs to be initialized.
        /// </summary>
        void Start();

        void Finish();
    }
}