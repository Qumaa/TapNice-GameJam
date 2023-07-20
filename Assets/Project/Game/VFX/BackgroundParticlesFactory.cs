using System.Runtime.InteropServices;
using UnityEngine;

namespace Project.Game.VFX
{
    [StructLayout(LayoutKind.Auto)]
    public readonly struct BackgroundParticlesFactory : IFactory<IBackgroundParticles>
    {
        private readonly GameObject _particlesPrefab;
        private readonly float _densityperUnit;

        public BackgroundParticlesFactory(GameObject particlesPrefab, float densityperUnit)
        {
            _particlesPrefab = particlesPrefab;
            _densityperUnit = densityperUnit;
        }

        public IBackgroundParticles CreateNew()
        {
            var obj = GameObject.Instantiate(_particlesPrefab);
            GameObject.DontDestroyOnLoad(obj);
            var particles = new BackgroundParticles(obj.GetComponent<ParticleSystem>(), _densityperUnit);
            particles.Deactivate();
            return particles;
        }
    }
}