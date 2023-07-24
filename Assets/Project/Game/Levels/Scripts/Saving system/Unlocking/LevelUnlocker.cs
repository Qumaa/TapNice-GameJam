using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;
#endif

namespace Project.Game.Levels
{
    public class LevelUnlocker : ILevelUnlocker
    {
        private readonly ILevelDescriptor[] _levels;
        private readonly UniversalSavingSystem<UnlockedLevelsData> _savingSystem;
        private readonly UnlockedLevelsData _unlockedLevelsData;

        public LevelUnlocker(ILevelDescriptor[] levels)
        {
            _levels = levels;
            _savingSystem = new UniversalSavingSystem<UnlockedLevelsData>(UnlockedLevelsData.Empty);
            _unlockedLevelsData = _savingSystem.LoadData(GetFilePath());
        }
        
        public void UnlockLevel(int levelIndex) =>
            UnlockLevel(LevelIndexToItsName(levelIndex));

        public bool IsLevelUnlocked(int levelIndex) =>
            IsLevelUnlocked(LevelIndexToItsName(levelIndex));

        public void UnlockLevel(string levelName) =>
            _unlockedLevelsData.SetUnlocked(levelName);

        public bool IsLevelUnlocked(string levelName) =>
            _unlockedLevelsData.IsUnlocked(levelName);

        private string LevelIndexToItsName(int levelIndex) =>
            _levels[levelIndex].LevelName;

        public void SaveLoadedData() =>
            _savingSystem.SaveData(_unlockedLevelsData, GetFilePath());

        private static string GetFilePath() =>
            LevelsDataPaths.Unlocking.FilePath;

        [Serializable]
        private record UnlockedLevelsData
        {
            private readonly Dictionary<string, bool> _unlockedTable = new();

            public void SetUnlocked(string levelName)
            {
                if (!_unlockedTable.TryAdd(levelName, true))
                    _unlockedTable[levelName] = true;
            }

            public bool IsUnlocked(string levelName) =>
                _unlockedTable.TryGetValue(levelName, out var unlocked) && unlocked;

            public static UnlockedLevelsData Empty() =>
                new();
        }
        
#if UNITY_EDITOR
        [MenuItem("Project/Clear Unlocked Levels")]
        private static void ClearUnlockedLevels() =>
            File.Delete(GetFilePath());

        [MenuItem("Project/Unlock All Levels")]
        private static void UnlockAllLevels()
        {
            const string _DESCRIPTORS_FOLDER_PATH = "/Project/Game/Levels/Descriptors";

            var files = Directory.GetFiles(Application.dataPath + _DESCRIPTORS_FOLDER_PATH, "*.asset");
            var descriptors = new ILevelDescriptor[files.Length];
            
            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i];
                var assetPath = "Assets" + file.Replace(Application.dataPath, "").Replace('\\', '/');
                var descriptor = AssetDatabase.LoadAssetAtPath<LevelDescriptor>(assetPath);
                
                descriptors[i] = descriptor;
            }

            var unlocker = new LevelUnlocker(descriptors);
            
            foreach(var descriptor in descriptors)
                unlocker.UnlockLevel(descriptor.LevelName);
            
            unlocker.SaveLoadedData();
        }
#endif
    }
}