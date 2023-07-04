using System;
using System.Collections.Generic;

namespace Project.Architecture
{
    public class GameStateMachine : IGameStateMachine
    {
        private Dictionary<Type, IGameState> _states;
        private IGameState _currentState;

        public GameStateMachine()
        {
            _states = new Dictionary<Type, IGameState>();
        }

        public IGameStateMachine AddState<T>(T state)
            where T : IGameState
        {
            _states.Add(typeof(T), state);
            return this;
        }

        public void SetState<T>()
            where T : IGameState
        {
            SetState(_states[typeof(T)]);
        }

        private void SetState(IGameState state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
        }
    }
}