using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = System.Object;

namespace Project.Game
{
    public class GameInputReader : MonoBehaviour, IGameInputService
    {
        public event Action OnScreenTouchInput;
        private List<RaycastResult> _uiRaycastBuffer;

        private void Awake()
        {
            _uiRaycastBuffer = new List<RaycastResult>();
        }

        private void Update()
        {
            if (HasTouched() && NotOverUI())
                OnScreenTouchInput?.Invoke();
        }

        private bool NotOverUI()
        {
            var eventSystem = EventSystem.current;

            if (ReferenceEquals(eventSystem, null))
                return true;
            
            var eventData = new PointerEventData(eventSystem);
#if UNITY_EDITOR
            eventData.position = Input.mousePosition;
#else
            eventData.position = Input.GetTouch(0).position;
#endif
            eventSystem.RaycastAll(eventData, _uiRaycastBuffer);

            return _uiRaycastBuffer.Count == 0;
        }

        private static bool HasTouched()
        {
#if UNITY_EDITOR
            return Input.GetMouseButtonDown(0);
#endif
            return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        }
    }
}