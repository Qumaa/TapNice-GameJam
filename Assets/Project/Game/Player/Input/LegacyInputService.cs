using System;
using UnityEngine;

namespace Project.Game.Player
{
    public class LegacyInputService : MonoBehaviour, IGameInputService
    {
        public event Action OnScreenTouchInput;

        private void Update()
        {
            if (Input.anyKeyDown)
                OnScreenTouchInput?.Invoke();
        }
    }
}