using System;
using System.Collections;
using UnityEngine;

namespace Project.Game.Player
{
    [RequireComponent(typeof(TrailRenderer))]
    public class TrailRendererWrapper : MonoBehaviour
    {
        private TrailRenderer _trailRenderer;
        private Coroutine _stopRoutine;

        private void Awake() =>
            _trailRenderer = GetComponent<TrailRenderer>();

        public event Action<TrailRendererWrapper> OnStopped;

        public void SetEmitting(bool emitting)
        {
            if (_trailRenderer.emitting == emitting)
                return;

            _trailRenderer.emitting = emitting;

            UpdateStopRoutine(emitting);
        }

        public void Clear() =>
            _trailRenderer.Clear();

        public void ClearAndDisableEmitting()
        {
            Clear();
            SetEmitting(false);
        }

        public void SetColor(Color color) =>
            _trailRenderer.startColor = _trailRenderer.endColor = color;

        public void SetActive(bool active) =>
            _trailRenderer.gameObject.SetActive(active);

        private void UpdateStopRoutine(bool emitting)
        {
            if (emitting)
                KillStopRoutine();
            else
                StartStopRoutine();
        }

        private void StartStopRoutine() =>
            _stopRoutine = StartCoroutine(StopRoutine(_trailRenderer.time));

        private void KillStopRoutine()
        {
            if (_stopRoutine != null)
                StopCoroutine(_stopRoutine);

            _stopRoutine = null;
        }

        private IEnumerator StopRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            OnStopped?.Invoke(this);
        }
    }
}