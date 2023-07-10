using System;
using Project.Game;

namespace Project.UI
{
    public interface IMainMenu
    {
        public event Action<int> OnLevelPlayPressed;
        void SetLevels(ILevelDescriptor[] levels);
    }
}
