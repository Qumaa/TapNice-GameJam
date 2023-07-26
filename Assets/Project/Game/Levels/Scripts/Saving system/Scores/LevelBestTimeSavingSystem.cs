
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
            _bestTimes.ValidateCapacity(_levelDescriptors.Length);
        }

        public LevelBestTime GetBestTime(int levelIndex) =>
            _bestTimes[levelIndex];

        public void SetBestTime(int levelIndex, LevelBestTime time) =>
            _bestTimes[levelIndex] = time.AsSeconds;

        public void SaveLoadedData() =>
            _savingSystem.SaveData(_bestTimes, GetFilePath());

        private LevelsBestTimeData EmptyDataFactory() =>
            new(_levelDescriptors.Length);

        private static string GetFilePath() =>
            LevelsDataPaths.Scores.FilePath;

        [Serializable]
        private record LevelsBestTimeData : SimpleTableData<float>
        {
            public LevelsBestTimeData(int capacity) : base(capacity) { }
        }

#if UNITY_EDITOR
        [MenuItem("Project/Clear Level Scores")]
        private static void ClearLevelScores() =>
            File.Delete(GetFilePath());
#endif
    }
}