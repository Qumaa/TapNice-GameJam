using System;
using Project.Game.Levels;

namespace Project.UI
{
    public interface IMainMenu : IGameUI
    {
        public event Action<int> OnLevelPlayPressed;
        public event Action OnQuitPressed;
        void SetLevels(ILevelDescriptor[] levels);
    }
}