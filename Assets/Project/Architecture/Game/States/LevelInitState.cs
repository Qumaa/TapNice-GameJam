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
        }

        public override void Enter()
        {
            _level.Start();
            _level.OnFinished += HandleLevelFinish;
        }

        public override void Exit()
        {
        }

        private void HandleLevelFinish(float time)
        {
            // _level.OnFinished -= HandleLevelFinish;
            
            Debug.Log($"Level finished from state with time {time}");
        }
    }
}