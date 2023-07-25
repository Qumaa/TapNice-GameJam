using System;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

namespace Project.Game.Levels
{
    public class LevelBestTimeSavingSystem : ILevelBestTimeService
    {
        private readonly ILevelDescriptor[] _levelDescriptors;
        private readonly UniversalSavingSystem<LevelsBestTimeData> _savingSystem;
        private readonly LevelsBestTimeData _bestTimes;

        public LevelBestTimeSavingSystem(ILevelDescriptor[] levelDescriptors)
        {
            _levelDescriptors = levelDescriptors;
            _savingSystem = new UniversalSavingSystem<LevelsBestTimeData>(EmptyDataFactory);
            _bestTimes = _savingSystem.LoadData(GetFilePath());
        }

        public LevelBestTime GetBestTime(string levelName)
        {
            var levelIndex = LevelNameToIndex(levelName);
            return _bestTimes[levelIndex];
        }

        public void SetBestTime(string levelName, LevelBestTime time) =>
            _bestTimes[LevelNameToIndex(levelName)] = time.AsSeconds;

        public void SaveLoadedData() =>
            _savingSystem.SaveData(_bestTimes, GetFilePath());
        
        private int LevelNameToIndex(string levelName)
        {
            for (var i = 0; i < _levelDescriptors.Length; i++)
                if (_levelDescriptors[i].LevelName == levelName)
                    return i;

            throw new ArgumentException();
        }

        private LevelsBestTimeData EmptyDataFactory() =>
            new(_levelDescriptors.Length);

        private static string GetFilePath() =>
            LevelsDataPaths.Scores.FilePath;

        [Serializable]
        private record LevelsBestTimeData
        {
            private readonly float[] _bestScoresTable;

            public float this[int levelIndex]
            {
                get => _bestScoresTable[levelIndex];
                set => _bestScoresTable[levelIndex] = value;
            }

            public LevelsBestTimeData(int levelsCount)
            {
                _bestScoresTable = new float[levelsCount];
            }
        }

#if UNITY_EDITOR
        [MenuItem("Project/Clear Level Scores")]
        private static void ClearLevelScores() =>
            File.Delete(GetFilePath());
#endif
    }
}