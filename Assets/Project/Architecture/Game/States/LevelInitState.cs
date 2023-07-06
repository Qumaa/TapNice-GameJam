using System;
using Project.Game;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Architecture
{
    public class LevelInitState : GameState
    {
        private readonly ICollisionHandler[] _handlersTable;
        
        public LevelInitState(IGame game, IGameStateMachine stateMachine) : base(game, stateMachine)
        {
            var items = Enum.GetNames(typeof(CollisionHandlerType)).Length;
            _handlersTable = new ICollisionHandler[items];
        }

        public override void Enter()
        {
            InitCollisionHandlers();
        }

        public override void Exit()
        {
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
        
        private ICollisionHandler GetCollisionHandler(CollisionHandlerType type) =>
            _handlersTable[(int) type] ??= CreateHandler(type);

        private static ICollisionHandler CreateHandler(CollisionHandlerType type) =>
            type switch
            {
                CollisionHandlerType.Default => new LevelCollisionHandler(),
                CollisionHandlerType.Finish => new FinishCollisionHandler(),
                CollisionHandlerType.Trampoline => new TrampolineCollisionHandler(),
                CollisionHandlerType.Discharger => new DischargerCollisionHandler(),
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}