using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class FunctionalMainMenuButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _levelNameLabel;
        private int _levelIndex;
        public event Action<int> OnPressed;

        private void OnEnable()
        {
            _button.onClick.AddListener(EmitEvent);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(EmitEvent);
        }

        public void SetLevel(string levelName, int index)
        {
            _levelNameLabel.text = GetLevelName(index, levelName);
            _levelIndex = index;
        }

        public void SetEnabled(bool enabled) =>
            _button.interactable = enabled;

        private static string GetLevelName(int index, string name) =>
            $"{index + 1}. {name}";

        private void EmitEvent() =>
            OnPressed?.Invoke(_levelIndex);
    }
}