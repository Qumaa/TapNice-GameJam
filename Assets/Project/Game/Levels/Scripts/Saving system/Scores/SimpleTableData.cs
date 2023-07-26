using System;

namespace Project.Game.Levels
{
    [Serializable]
    public abstract record SimpleTableData<T>
    {
        private T[] _table;

        public T this[int i]
        {
            get => _table[i];
            set => _table[i] = value;
        }

        protected SimpleTableData(int capacity)
        {
            _table = new T[capacity];
        }

        public void ValidateCapacity(int levelsCount)
        {
            if (_table.Length < levelsCount)
                Array.Resize(ref _table, levelsCount);
        }
    }
}