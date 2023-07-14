﻿using UnityEngine;

namespace Project.Game
{
    public class PlayerTrail : IPlayerTrail
    {
        private readonly IContainer<TrailRendererWrapper> _trailsContainer;
        private TrailRendererWrapper _currentTrail;

        public PlayerTrail(IFactory<TrailRendererWrapper> factory)
        {
            _trailsContainer = new Container<TrailRendererWrapper>(factory.CreateNew);
            
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
            _currentTrail.SetEmitting(true);

        public void Deactivate() =>
            _currentTrail.ClearAndDisableEmitting();

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