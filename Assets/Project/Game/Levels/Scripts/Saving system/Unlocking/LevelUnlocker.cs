﻿using System;

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
            _savingSystem = new UniversalSavingSystem<UnlockedLevelsData>(EmptyDataFactory);
            _unlockedLevelsData = _savingSystem.LoadData(GetFilePath());
            _unlockedLevelsData.ValidateCapacity(_levels.Length);
        }

        public void UnlockLevel(int levelIndex) =>
            _unlockedLevelsData[levelIndex] = true;

        public bool IsLevelUnlocked(int levelIndex) =>
            _unlockedLevelsData[levelIndex];


        public void SaveLoadedData() =>
            _savingSystem.SaveData(_unlockedLevelsData, GetFilePath());

        private UnlockedLevelsData EmptyDataFactory() =>
            new(_levels.Length);

        private static string GetFilePath() =>
            LevelsDataPaths.Unlocking.FilePath;

        [Serializable]
        private record UnlockedLevelsData : SimpleTableData<bool>
        {
            public UnlockedLevelsData(int capacity) : base(capacity) { }
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

            for (var i = 0; i < descriptors.Length; i++)
                unlocker.UnlockLevel(i);

            unlocker.SaveLoadedData();
        }
#endif
    }
}