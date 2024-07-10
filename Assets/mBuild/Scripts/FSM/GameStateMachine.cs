using System;
using System.Collections.Generic;

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

            bool firstState = false;
            if (_currentState == null) firstState = true;

            _states.Add(type, state);
            
            if (firstState) StartBehaviour(state);
        }

        public void GoTo<INewState>() where INewState : IGameState
        {
            Type type = typeof(INewState);

            if (type == _currentState.GetType()) return;

            if (_states.TryGetValue(type, out IGameState state))
            {
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

        public void UseActiveState() => _currentState?.Operate();

        private void StartBehaviour(IGameState state)
        {
            if (state == null)
                throw new InvalidOperationException($"First state is null");

            _currentState = state;
            _currentState.Enter();
            StateChanged?.Invoke();
        }
    }
}