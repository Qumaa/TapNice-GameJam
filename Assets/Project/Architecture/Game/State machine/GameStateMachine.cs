﻿using System;
using System.Collections.Generic;

namespace Project.Architecture
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableGameState> _states = new();
        private IExitableGameState _currentState;

        public IGameStateMachine AddState<T>(T state)
            where T : IExitableGameState
        {
            _states.Add(typeof(T), state);
            return this;
        }

        public void SetState<T>()
            where T : IEnterableGameState
        {
            SetStateInternal<T>().Enter();
        }

        public void SetState<T, TArg>(TArg arg)
            where T : IEnterableGameState<TArg>
        {
            SetStateInternal<T>().Enter(arg);
        }

        private T SetStateInternal<T>()
        {
            var state = _states[typeof(T)];
            _currentState?.Exit();
            _currentState = state;
            return (T) state;
        }
    }
}