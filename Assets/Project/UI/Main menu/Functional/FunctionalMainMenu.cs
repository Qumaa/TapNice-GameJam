using System;
using Project.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class FunctionalMainMenu : MonoBehaviour, IMainMenu
    {
        [SerializeField] private FunctionalMainMenuButton _buttonPrefab;
        [SerializeField] private Button _quitButton;
        [SerializeField] private RectTransform _buttonsParent;

        private FunctionalMainMenuButton[] _buttons;

        public event Action<int> OnLevelPlayPressed;
        public event Action OnQuitPressed;

        public void SetLevels(ILevelDescriptor[] levels)
        {
            _buttons = new FunctionalMainMenuButton[levels.Length];

            for (var i = 0; i < levels.Length; i++)
                CreateLevelButton(levels[i], i);
        }

        private void OnEnable()
        {
            _quitButton.onClick.AddListener(EmitQuitEvent);
        }

        private void OnDisable()
        {
            _quitButton.onClick.RemoveListener(EmitQuitEvent);
        }

        private void OnDestroy()
        {
#if UNITY_EDITOR
            if (_buttons == null)
                return;
#endif

            foreach (var button in _buttons)
                button.OnPressed -= EmitLevelEvent;
        }

        private void CreateLevelButton(ILevelDescriptor level, int index)
        {
            var button = CreateButtonObject();
            button.SetLevel(level.LevelName, index);
            button.OnPressed += EmitLevelEvent;

            _buttons[index] = button;
        }

        private FunctionalMainMenuButton CreateButtonObject() =>
            Instantiate(_buttonPrefab, _buttonsParent, false).GetComponent<FunctionalMainMenuButton>();

        private void EmitLevelEvent(int arg) =>
            OnLevelPlayPressed?.Invoke(arg);

        private void EmitQuitEvent() =>
            OnQuitPressed?.Invoke();
    }
}