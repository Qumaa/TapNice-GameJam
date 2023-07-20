using UnityEngine;

namespace Project.Game.Player.VFX
{
    public class PlayerTrailTransformContainer : MonoBehaviour
    {
        [SerializeField] private Transform _trailsTransform;

        public Transform TrailsTransform => _trailsTransform;
    }
}
