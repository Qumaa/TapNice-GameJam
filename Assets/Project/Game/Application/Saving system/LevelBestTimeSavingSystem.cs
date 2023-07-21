using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Project.Game.Levels
{
    public class LevelBestTimeSavingSystem : ILevelBestTimeService
    {
        private const string _FILES_RELATIVE_PATH = "Level Scores/";

        private readonly UniversalSavingSystem<LevelBestTimeStaticData> _savingSystem;
        private readonly Dictionary<string, LevelBestTimeStaticData> _bestScoresTable;

        public LevelBestTimeSavingSystem()
        {
            _savingSystem = new UniversalSavingSystem<LevelBestTimeStaticData>(LevelBestTimeStaticData.Empty);
            _bestScoresTable = new Dictionary<string, LevelBestTimeStaticData>();
        }

        public LevelBestTime GetBestTime(string levelName) =>
            _bestScoresTable.TryGetValue(levelName, out var bestTime) ? bestTime : LevelBestTime.Empty();

        public void SetBestTime(string levelName, LevelBestTime time)
        {
            if (_bestScoresTable.TryGetValue(levelName, out var savedTime))
            {
                savedTime.TimeInSeconds = time.AsSeconds;
                return;
            }

            savedTime = new LevelBestTimeStaticData(levelName, time);
            _bestScoresTable.Add(levelName, savedTime);
        }

        public void LoadSavedData()
        {
            var folderPath = GetFolderPath();
            
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePaths = Directory.GetFiles(folderPath);


            foreach (var filePath in filePaths)
            {
                var fileName = Path.GetFileName(filePath);
                var data = _savingSystem.LoadData(filePath);
                data.LevelName = fileName;

                _bestScoresTable.TryAdd(fileName, data);
            }
        }

        public void SaveLoadedData()
        {
            foreach (var levelData in _bestScoresTable)
                _savingSystem.SaveData(levelData.Value, GetLevelDataPath(levelData.Key));
        }

        private static string GetFolderPath() =>
            Path.Combine(Application.persistentDataPath, _FILES_RELATIVE_PATH);

        private static string GetLevelDataPath(string levelName) =>
            GetFolderPath() + levelName;

        [Serializable]
        private record LevelBestTimeStaticData
        {
            [NonSerialized] public string LevelName;
            public float TimeInSeconds;

            public LevelBestTimeStaticData(string levelName, float timeInSeconds)
            {
                TimeInSeconds = timeInSeconds;
                LevelName = levelName;
            }

            public LevelBestTimeStaticData(string levelName, LevelBestTime time) : this(levelName, time.AsSeconds) { }

            public static implicit operator LevelBestTime(LevelBestTimeStaticData data) =>
                new(data.TimeInSeconds);

            public static LevelBestTimeStaticData Empty() =>
                new(null, 0);
        }
    }
}