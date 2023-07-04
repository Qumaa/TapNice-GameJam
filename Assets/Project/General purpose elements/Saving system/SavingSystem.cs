namespace Project
{
    public abstract class SavingSystem<T> : ISavingSystem<T>
    {
        private readonly string _savePath;
        private T _cachedInstance;
        private bool _hasCachedInstance;
        
        protected SavingSystem(string savePath)
        {
            _savePath = savePath;
        }
        
        public void SaveData(T data)
        {
            SetCachedInstance(data);
            WriteInstanceToDisk(data, _savePath);
        }

        public T LoadData()
        {
            if (_hasCachedInstance)
                return _cachedInstance;

            var dataInstance = CanLoadFromDisk(_savePath) ? LoadInstanceFromDisk(_savePath) : CreateEmptyDataInstance();
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