using System.IO;
using UnityEngine;

namespace Project.Game.Levels
{
    public static class LevelsDataPaths
    {
        public static readonly string Root = Path.Combine(Application.persistentDataPath, "Levels Data/");

        private static void CreateFolderIfNecessary(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static class Scores
        {
            public const string FILE_NAME = "Scores";

            private static readonly string _filePath = Root + FILE_NAME;
            public static string FilePath
            {
                get
                {
                    CreateFolderIfNecessary(Root);
                    return _filePath;
                }
            }
        }
        
        public static class Unlocking
        {
            public const string FILE_NAME = "Unlocked";
            
            private static readonly string _filePath = Root + FILE_NAME;
            public static string FilePath
            {
                get
                {
                    CreateFolderIfNecessary(Root);
                    return _filePath;
                }
            }
        }
    }
}