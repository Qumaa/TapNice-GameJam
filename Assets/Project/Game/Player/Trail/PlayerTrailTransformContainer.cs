using UnityEngine;

namespace Project.Game
{
    public class PlayerTrailTransformContainer : MonoBehaviour
    {
        [SerializeField] private Transform _trailsTransform;

        public Transform TrailsTransform => _trailsTransform;
    }
}
