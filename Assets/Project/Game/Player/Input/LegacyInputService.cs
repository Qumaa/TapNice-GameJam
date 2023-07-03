using System;
using UnityEngine;

namespace Project.Game
{
    public class LegacyInputService : MonoBehaviour, IPlayerInputService
    {
        public event Action OnJumpInput;

        private void Update()
        {
            if (Input.anyKeyDown)
                OnJumpInput?.Invoke();
        }
    }
}