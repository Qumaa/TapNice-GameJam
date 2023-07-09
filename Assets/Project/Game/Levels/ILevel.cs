using System;
using UnityEngine;

namespace Project.Game
{
    public interface ILevel
    {
        IObservable<Vector2> Gravity { get; }
        /// <summary>
        /// Float argument is time elapsed since the <see cref="Start"/> method has been called.
        /// </summary>
        public event Action<float> OnFinished;

        /// <summary>
        /// Call this method when a scene is loaded and level needs to be initialized.
        /// </summary>
        void Start();

        void Finish();
    }
}