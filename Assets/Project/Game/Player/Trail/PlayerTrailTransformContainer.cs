using UnityEngine;

namespace Project
{
    public class PlayerTrailTransformContainer : MonoBehaviour
    {
        [SerializeField] private Transform _trailsTransform;

        public Transform TrailsTransform => _trailsTransform;
    }
}
