using UnityEngine;

namespace Project
{
    public record PlayerTrailRendererFactory : IFactory<TrailRenderer>
    {
        private readonly GameObject _prefab;
        private readonly Transform _container;

        public PlayerTrailRendererFactory(GameObject prefab, Transform container)
        {
            _prefab = prefab;
            _container = container;
        }

        public TrailRenderer CreateNew() =>
            GameObject.Instantiate(_prefab, _container, false).GetComponent<TrailRenderer>();
    }
}