using System;
using Cinemachine;
using UnityEngine;

namespace Project.Game.Levels
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class LevelSegment : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;

        private static LevelSegment _activeSegment;

        private void Awake() =>
            Deactivate(this);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsPlayer(other))
                SetAsActive(this);
        }

        private void SetActive(bool active) =>
            _virtualCamera.gameObject.SetActive(active);

        private static void SetAsActive(LevelSegment levelSegment)
        {
            if (_activeSegment != null)
                Deactivate(_activeSegment);
            
            _activeSegment = levelSegment;
            Activate(_activeSegment);
        }
        
        private static void Activate(LevelSegment levelSegment)
        {
            levelSegment.SetActive(true);
            levelSegment._virtualCamera.Priority = 1;
        }

        private static void Deactivate(LevelSegment levelSegment)
        {
            levelSegment.SetActive(false);
            levelSegment._virtualCamera.Priority = 0;
        }

        private static bool IsPlayer(Collider2D other) =>
            other.CompareTag(Tags.PLAYER);
    }
}