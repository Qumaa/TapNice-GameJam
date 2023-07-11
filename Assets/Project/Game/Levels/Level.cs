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
        
        public IObservable<Vector2> Gravity { get; }
        public float TimeElapsed => (float) (GetGameTime() - _startTime);
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
            ResetPlayer();
            InitCollisionHandlers();
        }

        public void Finish()
        {
            ResetPlayer();
            OnFinished?.Invoke();
            OnFinishedWithTime?.Invoke(TimeElapsed);
        }

        private void StartTimeCounting()
        {
            _player.OnJumped -= StartTimeCounting;
            _startTime = GetGameTime();
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
    }
}
