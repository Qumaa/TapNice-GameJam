using UnityEngine;

namespace Project.Game.VFX
{
    public class RippleContainer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public ParticleSystem ParticleSystem => _particleSystem;
    }
}
