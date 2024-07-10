using System;

namespace Infrastructure
{
    public interface IReadOnlyEventBus
    {
        public event Action OnFightStartButtonDown;

        public event Action OnFightStarted;

        public event Action OnFightEnd;
    }
}