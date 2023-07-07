using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Game
{
    public class TemporalMainMenu : MonoBehaviour, IMainMenu
    {
        [SerializeField] private Button _playButton;

        private void Start()
        {
            _playButton.onClick.AddListener(() => OnLevelPlayPressed?.Invoke(0));
        }

        public event Action<int> OnLevelPlayPressed;
    }
}