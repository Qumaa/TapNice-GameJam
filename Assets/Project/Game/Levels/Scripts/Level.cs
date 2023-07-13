using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Game
{
    public class Level : ILevel
    {
        private readonly ICollisionHandler[] _handlersTable;
        private readonly IPlayer _player;
        private double _startTime;
        private float _timeElapsedCached;
        private Func<float> _elapsedTimeStrategy;

        public IObservable<Vector2> Gravity { get; }
        public float TimeElapsed => _elapsedTimeStrategy();
        public event Action<float> OnFinishedWithTime;
        public event Action OnFinished;

        public Level(IObservable<Vector2> gravity, IPlayer player)
        {
            Gravity = gravity;
            _player = player;
            
            var items = Enum.GetNames(typeof(CollisionHandlerType)).Length;
            _handlersTable = new ICollisionHandler[items];
        }

        public void Start()
        {
            _player.OnJumped += StartTimeCounting;
            InitCollisionHandlers();
            
            Reset();
        }

        public void Finish()
        {
            _timeElapsedCached = TimeElapsed;
            _elapsedTimeStrategy = CachedTimeStrategy;
            
            OnFinished?.Invoke();
            OnFinishedWithTime?.Invoke(TimeElapsed);
        }

        public void Reset()
        {
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

        private void InitCollisionHandlers()
        {
            var handlerContainers =
                Object.FindObjectsByType<SceneCollisionHandlerContainer>(FindObjectsInactive.Include,
                    FindObjectsSortMode.None);

            if (handlerContainers.Length == 0)
                return;

            foreach (var handlerContainer in handlerContainers)
                handlerContainer.SetHandler(GetCollisionHandler(handlerContainer.HandlerType));
        }

        private void ResetPlayer()
        {
            var spawnPoint = Object.FindObjectOfType<PlayerSpawnPoint>();
            
            _player.Reset(spawnPoint.Position, spawnPoint.PlayerDirection);
        }

        private ICollisionHandler GetCollisionHandler(CollisionHandlerType type) =>
            _handlersTable[(int) type] ??= CreateCollisionHandler(type);

        private ICollisionHandler CreateCollisionHandler(CollisionHandlerType type) =>
            type switch
            {
                CollisionHandlerType.Default => new LevelCollisionHandler(),
                CollisionHandlerType.Finish => new FinishCollisionHandler(this),
                CollisionHandlerType.Trampoline => new TrampolineCollisionHandler(),
                CollisionHandlerType.Discharger => new DischargerCollisionHandler(),
                _ => throw new ArgumentOutOfRangeException()
            };

        private static double GetGameTime() =>
            Time.timeSinceLevelLoadAsDouble;

        private float CachedTimeStrategy() =>
            _timeElapsedCached;

        private float CalculateTimeStrategy() =>
            (float) (GetGameTime() - _startTime);
    }
}
