﻿namespace Project.Architecture
{
    public class RestartLevelState : GameState
    {
        public RestartLevelState(IGame game, IGameStateMachine stateMachine) : base(game, stateMachine) { }

        public override void Enter()
        {
            _game.LoadedLevel.Reset();
            _stateMachine.SetState<GameLoopState>();
        }

        public override void Exit() { }
    }
}