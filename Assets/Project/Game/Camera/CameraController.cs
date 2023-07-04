using UnityEngine;

namespace Project.Game
{
    public class CameraController : ICameraController
    {
        public Vector2 Position
        {
            get => ControlledCamera.transform.position;
            set => SetPosition(value);
        }


        private float _widthInUnits;

        public Camera ControlledCamera { get; }
        public float ViewportDepth { get; }


        public CameraController(Camera controlledCamera, float viewportDepth)
        {
            ControlledCamera = controlledCamera;
            ViewportDepth = viewportDepth;
            UpdateViewportDepth();
        }

        private void SetWidthInUnits(float units)
        {
            _widthInUnits = units;

            ControlledCamera.orthographicSize = _widthInUnits / (2 * ControlledCamera.aspect);
        }

        private void UpdateViewportDepth() =>
            SetPosition(Position);

        private void SetPosition(Vector2 position) =>
            ControlledCamera.transform.position = new Vector3(position.x, position.y, -ViewportDepth);
    }
}