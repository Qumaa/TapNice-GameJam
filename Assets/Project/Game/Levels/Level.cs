using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Game
{
    public class Level : ILevel
    {
        private readonly ICollisionHandler[] _handlersTable;
        private readonly IPlayer _player;
        
        public IObservable<Vector2> Gravity { get; }
        public event Action<float> OnFinished;

        public Level(IObservable<Vector2> gravity, IPlayer player)
        {
            Gravity = gravity;
            _player = player;
            
            var items = Enum.GetNames(typeof(CollisionHandlerType)).Length;
            _handlersTable = new ICollisionHandler[items];
        }

        public void Start()
        {
            _player.Activate();
            ResetPlayer();
            InitCollisionHandlers();
        }

        public void Finish()
        {
            // todo: time counting
            ResetPlayer();
            OnFinished?.Invoke(0);
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
    }
}
