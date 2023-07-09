using System;
using UnityEngine;

namespace Project.Game
{
    public class Level : ILevel
    {
        public IObservable<Vector2> Gravity { get; }
        public event Action<float> OnFinished;

        public Level(IObservable<Vector2> gravity)
        {
            Gravity = gravity;
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Finish()
        {
            // todo: time counting
            OnFinished?.Invoke(0);
        }
    }
}
