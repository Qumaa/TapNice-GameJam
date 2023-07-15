using System;
using System.Collections.Generic;

namespace Project.Architecture.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableGameState> _states = new();
        private IExitableGameState _currentState;

        public void AddSingleState<T>(T state) where T : IExitableGameState =>
            _states.Add(typeof(T), state);

        public void SetState<T>() where T : IEnterableGameState =>
            SetStateInternal<T>().Enter();

        public void SetState<T, TArg>(TArg arg) where T : IEnterableGameState<TArg> =>
            SetStateInternal<T>().Enter(arg);

        private T SetStateInternal<T>()
        {
            var state = _states[typeof(T)];
            _currentState?.Exit();
            _currentState = state;
            return (T) state;
        }
    }
}