using System;
using Project.Game.Player;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Game.Levels
{
    public class Level : ILevel
    {
        private readonly IPlayer _player;
        private double _startTime;
        private float _timeElapsedCached;
        private Func<float> _elapsedTimeStrategy;

        public float TimeElapsed => _elapsedTimeStrategy();
        public event Action OnStarted;
        public event Action OnRestarted;
        public event Action<float> OnFinishedWithTime;
        public event Action OnFinished;

        public Level(IPlayer player)
        {
            _player = player;
        }

        public void Start(string levelName)
        {
            Reset();

            OnStarted?.Invoke();
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
    }
}