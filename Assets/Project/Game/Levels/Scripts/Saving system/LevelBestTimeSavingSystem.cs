using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Project.Game.Levels
{
    public class LevelBestTimeSavingSystem : ILevelBestTimeService
    {
        private const string _FILE_RELATIVE_PATH = "Level Scores/";
        private const string _FILE_NAME = "Scores";

        private readonly UniversalSavingSystem<LevelsBestTimeData> _savingSystem;
        private LevelsBestTimeData _data;

        public LevelBestTimeSavingSystem()
        {
            _savingSystem = new UniversalSavingSystem<LevelsBestTimeData>(LevelsBestTimeData.Empty);
        }

        public LevelBestTime GetBestTime(string levelName) =>
            _data.GetBestTime(levelName);

        public void SetBestTime(string levelName, LevelBestTime time) =>
            _data.SetBestTime(levelName, time.AsSeconds);

        public void LoadSavedData() =>
            _data ??= _savingSystem.LoadData(GetFilePath());

        public void SaveLoadedData() =>
            _savingSystem.SaveData(_data, GetFilePath());

        private static string GetFilePath()
        {
            var path = Path.Combine(Application.persistentDataPath, _FILE_RELATIVE_PATH);
            
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            
            return path + _FILE_NAME;
        }
        
        [Serializable]
        private record LevelsBestTimeData
        {
            private readonly Dictionary<string, float> _bestScoresTable = new();
            
            public void SetBestTime(string levelName, float time)
            {
                if (!_bestScoresTable.TryAdd(levelName, time))
                    _bestScoresTable[levelName] = time;
            }
            
            public float GetBestTime(string levelName) =>
                _bestScoresTable.TryGetValue(levelName, out var bestTime) ? bestTime : 0;

            public static LevelsBestTimeData Empty() =>
                new();
        }

#if UNITY_EDITOR
        [MenuItem("Project/Clear Level Scores")]
        private static void ClearLevelScores() =>
            File.Delete(GetFilePath());
#endif
    }
}