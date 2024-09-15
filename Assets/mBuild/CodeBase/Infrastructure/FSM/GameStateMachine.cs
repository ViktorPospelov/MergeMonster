using System.Collections.Generic;
using System;

namespace FSM
{
    public class GameStateMachine : IReadOnlyStateMachine
    {
        public event Action StateChanged;

        public IGameState GameState => _currentState;

        private Dictionary<Type, IGameState> _states = new Dictionary<Type, IGameState>();
        private IGameState _currentState;

        public void AddState(IGameState state)
        {
            Type type = state.GetType();
            if (_states.ContainsKey(type))
                throw new InvalidOperationException($"State by type \"{nameof(type)}\" has already in dictionary");
            
            _states.Add(type, state);
        }

        public void GoTo<TNewState>() where TNewState : IGameState
        {
            Type type = typeof(TNewState);

            if (_currentState != null)
                if (type == _currentState.GetType()) return;

            if (_states.TryGetValue(type, out IGameState state))
            {
                if (_currentState != null) 
                    _currentState.Exit();
                
                _currentState = state;
                StateChanged?.Invoke();
                _currentState.Enter();
            }
            else
            {
                throw new InvalidOperationException($"State \"{nameof(type)}\" is not intialized in dictionary");
            }
        }

        public void Update() => _currentState?.Operate();
    }
}