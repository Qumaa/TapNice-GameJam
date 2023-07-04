using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Project
{
    public abstract class BinaryFormatterSavingSystem<T> : SavingSystem<T>
    {
        private readonly BinaryFormatter _formatter;

        protected BinaryFormatterSavingSystem(string savePath) :
            base(savePath)
        {
            _formatter = new BinaryFormatter();
        }

        protected override void WriteInstanceToDisk(T data, string filePath)
        {
            using var fileStream = File.Create(filePath);
            _formatter.Serialize(fileStream, data);
        }

        protected override bool CanLoadFromDisk(string filePath) =>
            File.Exists(filePath);

        protected override T LoadInstanceFromDisk(string filePath)
        {
            using FileStream fileStream = File.Open(filePath, FileMode.Open);
            return (T) _formatter.Deserialize(fileStream);
        }
    }
}