using System;

namespace Project.Game
{
    public interface IMainMenu
    {
        public event Action<int> OnLevelPlayPressed;
        void SetLevels(ILevelDescriptor[] levels);
    }
}