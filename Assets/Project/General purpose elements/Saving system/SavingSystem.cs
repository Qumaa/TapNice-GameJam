namespace Project
{
    public abstract class SavingSystem<T> : ISavingSystem<T>
    {
        private T _cachedInstance;
        private bool _hasCachedInstance;

        public void SaveData(T data, string path)
        {
            SetCachedInstance(data);
            WriteInstanceToDisk(data, path);
        }

        public T LoadData(string path)
        {
            if (_hasCachedInstance)
                return _cachedInstance;

            var dataInstance = CanLoadFromDisk(path) ? LoadInstanceFromDisk(path) : CreateEmptyDataInstance();
            SetCachedInstance(dataInstance);
            return dataInstance;
        }

        private void SetCachedInstance(T dataInstance)
        {
            _cachedInstance = dataInstance;
            _hasCachedInstance = true;
        }

        protected abstract void WriteInstanceToDisk(T data, string filePath);
        protected abstract bool CanLoadFromDisk(string filePath);
        protected abstract T CreateEmptyDataInstance();
        protected abstract T LoadInstanceFromDisk(string filePath);
    }
}