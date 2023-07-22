using System;
using Project.Game.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class FunctionalMainMenu : GameUI, IMainMenu
    {
        [SerializeField] private FunctionalMainMenuButton _buttonPrefab;
        [SerializeField] private Button _quitButton;
        [SerializeField] private RectTransform _buttonsParent;

        private FunctionalMainMenuButton[] _buttons;

        public event Action<int> OnLevelPlayPressed;
        public event Action OnQuitPressed;

        protected override void Awake()
        {
            base.Awake();
            _quitButton.onClick.AddListener(EmitQuitEvent);
        }

        public void SetLevels(ILevelDescriptor[] levels, ILevelUnlocker levelUnlocker)
        {
            _buttons = new FunctionalMainMenuButton[levels.Length];

            for (var i = 0; i < levels.Length; i++)
                CreateLevelButton(levels[i], i, levels[i].UnlockedByDefault || levelUnlocker.IsLevelUnlocked(i));
        }

        protected override void OnDelete()
        {
            _quitButton.onClick.RemoveListener(EmitQuitEvent);
            ReleaseButtons();
        }

        private void ReleaseButtons()
        {
            foreach (var button in _buttons)
                button.OnPressed -= EmitLevelEvent;
        }

        private void CreateLevelButton(ILevelDescriptor level, int index, bool enabled)
        {
            var button = CreateButtonObject();
            button.SetLevel(level.LevelName, index);
            button.OnPressed += EmitLevelEvent;
            button.SetEnabled(enabled);

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