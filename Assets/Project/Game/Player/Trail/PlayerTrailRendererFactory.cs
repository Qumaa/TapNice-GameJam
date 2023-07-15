using Project.Game.Player;
using UnityEngine;

namespace Project
{
    public record PlayerTrailRendererFactory : IFactory<TrailRendererWrapper>
    {
        private readonly GameObject _prefab;
        private readonly Transform _container;
        private readonly float _lifetime;

        public PlayerTrailRendererFactory(GameObject prefab, Transform container, float lifetime)
        {
            _prefab = prefab;
            _container = container;
            _lifetime = lifetime;
        }

        public TrailRendererWrapper CreateNew()
        {
            var obj = Object.Instantiate(_prefab, _container, false);
            obj.GetComponent<TrailRenderer>().time = _lifetime;
            return obj.GetComponent<TrailRendererWrapper>();
        }
    }
}