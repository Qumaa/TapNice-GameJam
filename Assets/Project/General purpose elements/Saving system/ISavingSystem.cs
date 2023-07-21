namespace Project
{
    public interface ISavingSystem<T>
    {
        void SaveData(T data, string path);
        T LoadData(string path);
    }
}
