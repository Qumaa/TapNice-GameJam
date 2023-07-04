namespace Project
{
    public interface ISavingSystem<T>
    {
        void SaveData(T data);
        T LoadData();
    }
    
}
