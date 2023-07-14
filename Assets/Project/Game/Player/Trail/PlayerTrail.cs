using System.Linq;
using UnityEngine;

namespace Project
{
    public class PlayerTrail : IPlayerTrail
    {
        private readonly IFactory<TrailRenderer> _factory;
        private readonly IContainer<TrailRendererWrapper> _trails;
        private TrailRendererWrapper _currentTrail;

        public PlayerTrail(IFactory<TrailRenderer> factory)
        {
            _factory = factory;
            _trails = new Container<TrailRendererWrapper>(CreateTrail);
            _currentTrail = _trails.Resolve();
        }

        public void Reset()
        {
            foreach (var trail in _trails.All.Reverse())
            {
                trail.Reset();
                _trails.Pool(trail);
            }

            _currentTrail = _trails.Resolve();
        }

        public void Activate()
        {
            _currentTrail.SetEmitting(true);
        }

        public void Deactivate()
        {
            _currentTrail.Reset();
            _currentTrail.SetEmitting(false);
        }

        public void SetColor(Color color)
        {
            _currentTrail.SetColor(color);
        }

        public void SetLifetime(float lifetime)
        {
            foreach (var trail in _trails.All)
                trail.SetLifetime(lifetime);
        }

        private TrailRendererWrapper CreateTrail() =>
            new(_factory.CreateNew());

        private class TrailRendererWrapper
        {
            private readonly TrailRenderer _trailRenderer;

            public TrailRendererWrapper(TrailRenderer trailRenderer)
            {
                _trailRenderer = trailRenderer;
            }

            public void SetEmitting(bool emitting) =>
                _trailRenderer.emitting = emitting;

            public void Reset() =>
                _trailRenderer.Clear();

            public void SetColor(Color color) =>
                _trailRenderer.startColor = _trailRenderer.endColor = color;

            public void SetLifetime(float lifetime) =>
                _trailRenderer.time = lifetime;
        }
    }
}