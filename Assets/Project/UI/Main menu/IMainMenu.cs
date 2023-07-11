using System;
using Project.Game;

namespace Project.UI
{
    public interface IMainMenu
    {
        public event Action<int> OnLevelPlayPressed;
        public event Action OnQuitPressed;
        void SetLevels(ILevelDescriptor[] levels);
    }
}