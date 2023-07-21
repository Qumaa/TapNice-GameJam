using System;
using Project.Configs;
using Project.Game.Levels;

namespace Project.Game.CollisionHandling
{
    public class CollisionHandlerResolver : ICollisionHandlerResolver
    {
        private readonly ICollisionHandler[] _handlersTable;
        private readonly ILevel _level;
        private readonly PlayerConfig _playerConfig;
        
        public ICollisionHandler Resolve(CollisionHandlerType type) =>
            GetCollisionHandler(type);

        private CollisionHandlerResolver()
        {
            var items = Enum.GetNames(typeof(CollisionHandlerType)).Length;
            _handlersTable = new ICollisionHandler[items];
        }
        
        public CollisionHandlerResolver(ILevel level, PlayerConfig playerConfig) : this()
        {
            _level = level;
            _playerConfig = playerConfig;
        }

        private ICollisionHandler GetCollisionHandler(CollisionHandlerType type) =>
            _handlersTable[(int) type] ??= CreateCollisionHandler(type);

        private ICollisionHandler CreateCollisionHandler(CollisionHandlerType type) =>
            type switch
            {
                CollisionHandlerType.Default => new LevelCollisionHandler(),
                CollisionHandlerType.Finish => new FinishCollisionHandler(_level, _playerConfig.FinishColor),
                CollisionHandlerType.Trampoline => new TrampolineCollisionHandler(),
                CollisionHandlerType.Discharger => new DischargerCollisionHandler(),
                CollisionHandlerType.Bouncer => new BouncerCollisionHandler(),
                CollisionHandlerType.Booster => new BoosterCollisionHandler(),
                CollisionHandlerType.Restart => new RestartCollisionHandler(_level),
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}