using Project.Game;
using Project.Game.CollisionHandling;
using UnityEngine;

namespace Project.Architecture.States
{
    public class LevelInitState : GameState
    {
        private readonly ICollisionHandlerResolver _handlerResolver;

        public LevelInitState(IGame game, IGameStateMachine stateMachine, ICollisionHandlerResolver handlerResolver) : 
            base(game, stateMachine)
        {
            _handlerResolver = handlerResolver;
        }

        public override void Enter()
        {
            InitCollisionHandlers();
            _game.LoadedLevel.Start();
            
            MoveNext();
        }

        public override void Exit() { }

        private void MoveNext() =>
            _stateMachine.SetState<GameLoopState>();

        private void InitCollisionHandlers()
        {
            var handlerContainers =
                Object.FindObjectsByType<SceneCollisionHandlerContainer>(FindObjectsInactive.Include,
                    FindObjectsSortMode.None);

            if (handlerContainers.Length == 0)
                return;

            foreach (var handlerContainer in handlerContainers)
                handlerContainer.SetHandler(_handlerResolver.Resolve(handlerContainer.HandlerType));
        }
    }
}