using System;

namespace FSM
{
    public interface IReadOnlyStateMachine
    {
        public event Action StateChanged;

        public IGameState GameState { get; }
    }
}