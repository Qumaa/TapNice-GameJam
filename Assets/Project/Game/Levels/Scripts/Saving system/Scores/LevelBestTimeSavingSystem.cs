using System;
using System.Collections.Generic;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Project.Game.Levels
{
    public class LevelBestTimeSavingSystem : ILevelBestTimeService
    {
        private readonly UniversalSavingSystem<LevelsBestTimeData> _savingSystem;
        private readonly LevelsBestTimeData _bestTimes;

        public LevelBestTimeSavingSystem()
        {
            _savingSystem = new UniversalSavingSystem<LevelsBestTimeData>(LevelsBestTimeData.Empty);
            _bestTimes = _savingSystem.LoadData(GetFilePath());
        }

        public LevelBestTime GetBestTime(string levelName) =>
            _bestTimes[levelName];

        public void SetBestTime(string levelName, LevelBestTime time) =>
            _bestTimes[levelName] = time.AsSeconds;

        public void SaveLoadedData() =>
            _savingSystem.SaveData(_bestTimes, GetFilePath());

        private static string GetFilePath() =>
            LevelsDataPaths.Scores.FilePath;

        [Serializable]
        private record LevelsBestTimeData
        {
            private readonly Dictionary<string, float> _bestScoresTable = new();

            public float this[string levelName]
            {
                get => GetBestTime(levelName);
                set => SetBestTime(levelName, value);
            }

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