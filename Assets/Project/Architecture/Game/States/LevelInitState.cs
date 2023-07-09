using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public class LevelInitState : GameState
    {
        private readonly ILevel _level;
        
        public LevelInitState(IGame game, IGameStateMachine stateMachine) : base(game, stateMachine)
        {
            _level = new Level(new Observable<Vector2>(Physics2D.gravity), _game.Player);
            _game.LoadedLevel = _level;
        }

        public override void Enter()
        {
            _level.Start();
            MoveNext();
        }

        public override void Exit()
        {
        }

        private void MoveNext() =>
            _stateMachine.SetState<GameLoopState>();
    }
}