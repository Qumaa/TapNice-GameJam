using UnityEngine;

namespace Project.Game
{
    public class PlayerTrail : IPlayerTrail
    {
        private readonly IContainer<TrailRendererWrapper> _trailsContainer;
        private TrailRendererWrapper _currentTrail;

        public PlayerTrail(IContainer<TrailRendererWrapper> container)
        {
            _trailsContainer = container;
            
            _trailsContainer.OnItemResolved += x => x.SetEmitting(true);
            _trailsContainer.OnItemPooled += x =>
            {
                x.ClearAndDisableEmitting();
                x.SetActive(false);
            };
            _trailsContainer.OnItemReleased += x => x.SetActive(true);
            _trailsContainer.OnItemCreated += x => x.OnStopped += HandleTrailStopped;

            _currentTrail = _trailsContainer.Resolve();
        }

        public void Reset()
        {
            _trailsContainer.PoolActiveExcept(_currentTrail);
            
            _currentTrail.Clear();
        }

        public void Activate() =>
            _currentTrail = _trailsContainer.Resolve();

        public void Deactivate()
        {
            _trailsContainer.PoolActive();
            _currentTrail = null;
        }

        public void SetColor(Color color)
        {
            _currentTrail.SetEmitting(false);
            _currentTrail = _trailsContainer.Resolve();
            _currentTrail.SetColor(color);
        }

        private void HandleTrailStopped(TrailRendererWrapper stoppedTrail) =>
            _trailsContainer.Pool(stoppedTrail);
    }
}