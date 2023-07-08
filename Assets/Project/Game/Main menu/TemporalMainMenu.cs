﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Game
{
    public class TemporalMainMenu : MonoBehaviour, IMainMenu
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private TextMeshProUGUI _buttonText;

        private void Start()
        {
            _playButton.onClick.AddListener(() => OnLevelPlayPressed?.Invoke(0));
        }

        public void SetButtonText(string text) =>
            _buttonText.text = text;

        public event Action<int> OnLevelPlayPressed;
    }
}