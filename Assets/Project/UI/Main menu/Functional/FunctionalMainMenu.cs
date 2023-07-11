using System;
using Project.Game;
using UnityEngine;

namespace Project.UI
{
    public class FunctionalMainMenu : MonoBehaviour, IMainMenu
    {
        [SerializeField] private FunctionalMainMenuButton _buttonPrefab;
        [SerializeField] private RectTransform _buttonsParent;
        
        private FunctionalMainMenuButton[] _buttons;
        
        public event Action<int> OnLevelPlayPressed;

        public void SetLevels(ILevelDescriptor[] levels)
        {
            _buttons = new FunctionalMainMenuButton[levels.Length];

            for (var i = 0; i < levels.Length; i++)
                CreateLevelButton(levels[i], i);
        }

        private void OnDestroy()
        {
#if UNITY_EDITOR
            if (_buttons == null)
                return;
#endif
            
            foreach (var button in _buttons)
                button.OnPressed -= EmitEvent;
        }

        private void CreateLevelButton(ILevelDescriptor level, int index)
        {
            var button = CreateButtonObject();
            button.SetLevel(level.LevelName, index);
            button.OnPressed += EmitEvent;

            _buttons[index] = button;
        }

        private FunctionalMainMenuButton CreateButtonObject() =>
            Instantiate(_buttonPrefab, _buttonsParent, false).GetComponent<FunctionalMainMenuButton>();

        private void EmitEvent(int arg) =>
            OnLevelPlayPressed?.Invoke(arg);
    }
}