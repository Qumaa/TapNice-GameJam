using System;
using Project.Configs;
using UnityEngine;

namespace Project.Game.VFX
{
    public class BackgroundParticles : MonoBehaviour
    {
        [SerializeField] private VFXConfig _vfxConfig;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Transform _lowerBound;
        [SerializeField] private Transform _upperBound;

        private ParticleSystem.Particle[] _particles;
        private Vector2 _size;

        private void Start() =>
            Init(_lowerBound.position, _upperBound.position);

        private void Update()
        {
            var activeParticles = GetActiveParticlesCount();

            var halfSize = _size / 2f;
            for (var i = 0; i < activeParticles; i++)
                UpdateParticlePosition(i, halfSize);

            SetUpdatedParticles(activeParticles);
        }

        private void Init(Vector2 areaFrom, Vector2 areaTo)
        {
            SetCenterPosition((areaFrom + areaTo) / 2f);

            _size = areaTo - areaFrom;
            var shape = _particleSystem.shape;
            shape.scale = new Vector3(_size.x, _size.y, shape.scale.z);
            
            UpdateParticlesDensity();
        }

        private int GetActiveParticlesCount()
        {
            ReallocateParticlesBufferIfNeeded();
            var particlesCount = _particleSystem.GetParticles(_particles);
            return particlesCount;
        }

        private void UpdateParticlePosition(int particleIndex, Vector2 halfSize) =>
            _particles[particleIndex].position = CalculateParticlePosition(_particles[particleIndex], halfSize);

        private void SetUpdatedParticles(int activeParticles) =>
            _particleSystem.SetParticles(_particles, activeParticles);

        private Vector2 CalculateParticlePosition(ParticleSystem.Particle particle, Vector2 halfSize)
        {
            var localSpace = _particleSystem.transform.InverseTransformPoint(particle.position);

            localSpace.x = Mathf.Repeat(localSpace.x + halfSize.x, _size.x) - halfSize.x;
            localSpace.y = Mathf.Repeat(localSpace.y + halfSize.y, _size.y) - halfSize.y;

            return _particleSystem.transform.TransformPoint(localSpace);
        }

        private void ReallocateParticlesBufferIfNeeded()
        {
            var max = _particleSystem.main.maxParticles;
            if (_particles == null || _particles.Length < max)
                _particles = new ParticleSystem.Particle[max];
        }

        private void SetCenterPosition(Vector2 position) =>
            _particleSystem.transform.position = position;

        private void UpdateParticlesDensity()
        {
            var lifetimeCurve = _particleSystem.main.startLifetime;
            var lifetimeAvg = (lifetimeCurve.constantMin + lifetimeCurve.constantMax) / 2f;

            var area = _size.x * _size.y;

            var emissionRate = _vfxConfig.DensityPerUnit * area / lifetimeAvg;

            var emissionModule = _particleSystem.emission;
            emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(emissionRate);
        }
    }
}