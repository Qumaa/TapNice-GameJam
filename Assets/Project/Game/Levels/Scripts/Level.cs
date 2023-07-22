using System;
using Project.Game.Player;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Game.Levels
{
    public class Level : ILevel
    {
        private readonly IPlayer _player;
        private readonly ILevelBestTimeService _savingSystem;
        private double _startTime;
        private float _timeElapsedCached;
        private Func<float> _elapsedTimeStrategy;

        public float TimeElapsed => _elapsedTimeStrategy();
        public string Name { get; set; }
        public event Action OnStarted;
        public event Action OnRestarted;
        public event Action<float> OnFinishedWithTime;
        public event Action OnFinished;

        public Level(IPlayer player, ILevelBestTimeService savingSystem)
        {
            _player = player;
            _savingSystem = savingSystem;
        }

        public void Start()
        {
            Reset();
            
            OnStarted?.Invoke();

            LogBestTime();
        }

        public void Restart()
        {
            Reset();
            OnRestarted?.Invoke();
            OnStarted?.Invoke();
        }

        public void Finish()
        {
            StopCountingTime();
            UpdateBestTime();
            LogBestTime();
            
            OnFinishedWithTime?.Invoke(TimeElapsed);
            OnFinished?.Invoke();
        }

        public void StopCountingTime()
        {
            _timeElapsedCached = TimeElapsed;
            _elapsedTimeStrategy = CachedTimeStrategy;
        }

        public void Reset()
        {
            _player.OnJumped += StartTimeCounting;
            _timeElapsedCached = 0;
            ResetPlayer();
            _elapsedTimeStrategy = CachedTimeStrategy;
        }

        private void StartTimeCounting()
        {
            _player.OnJumped -= StartTimeCounting;
            _startTime = GetGameTime();
            _elapsedTimeStrategy = CalculateTimeStrategy;
        }

        private void ResetPlayer()
        {
            var spawnPoint = Object.FindObjectOfType<PlayerSpawnPoint>();
            
            _player.Reset(spawnPoint.Position, spawnPoint.PlayerDirection);
        }

        private static double GetGameTime() =>
            Time.timeSinceLevelLoadAsDouble;

        private float CachedTimeStrategy() =>
            _timeElapsedCached;

        private float CalculateTimeStrategy() =>
            (float) (GetGameTime() - _startTime);

        private void UpdateBestTime()
        {
            var previous = _savingSystem.GetBestTime(Name);
            
            if (TimeElapsed < previous.AsSeconds || previous.IsEmpty)
                _savingSystem.SetBestTime(Name, TimeElapsed);
        }

        private void LogBestTime() =>
            Debug.Log($"Level {Name} | Best time : {_savingSystem.GetBestTime(Name).AsSeconds}");
    }
}
