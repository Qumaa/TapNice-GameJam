using System;
using Project.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class TemporalMainMenu : MonoBehaviour, IMainMenu
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private TextMeshProUGUI _buttonText;

        private void Start()
        {
            _playButton.onClick.AddListener(() => OnLevelPlayPressed?.Invoke(0));
        }

        private void SetButtonText(string text) =>
            _buttonText.text = text;

        public event Action<int> OnLevelPlayPressed;
        public void SetLevels(ILevelDescriptor[] levels) =>
            SetButtonText(levels[0].LevelName);
    }
}