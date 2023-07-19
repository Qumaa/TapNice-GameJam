using UnityEngine;

namespace Project.Game.VFX
{
    public class ParticlePlayerRippleVFX : IPlayerRippleVFX
    {
        private readonly ParticleSystem _particleSystem;

        public ParticlePlayerRippleVFX(ParticleSystem particleSystem)
        {
            _particleSystem = particleSystem;
        }

        public void PlayRipple(Vector2 position, Color color)
        {
            var emitParams = new ParticleSystem.EmitParams
            {
                position = position,
                startColor = color
            };
            _particleSystem.Emit(emitParams, 1);
        }

        public void Reset() =>
            _particleSystem.Clear();
    }
}