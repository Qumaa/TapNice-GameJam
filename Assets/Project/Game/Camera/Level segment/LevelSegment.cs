using Cinemachine;
using UnityEngine;

namespace Project.Game.Levels
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class LevelSegment : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;

        private static LevelSegment _activeSegment;
        
        private void Start() =>
            SetActive(false);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsPlayer(other))
                SetSegmentAsActive(this);
        }

        private void SetActive(bool active) =>
            _virtualCamera.gameObject.SetActive(active);

        private static void SetSegmentAsActive(LevelSegment levelSegment)
        {
            if (_activeSegment != null)
                _activeSegment.SetActive(false);
            
            _activeSegment = levelSegment;
            _activeSegment.SetActive(true);
        }

        private static bool IsPlayer(Collider2D other) =>
            other.CompareTag(Tags.PLAYER);
    }
}