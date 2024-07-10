using System;

namespace Infrastructure
{
    public class EventBus : IReadOnlyEventBus
    {
        public event Action OnFightStartButtonDown;
        public event Action OnFightStarted;
        public event Action OnFightEnd;

        public void InvokeOnFightStartButtonDown() => OnFightStartButtonDown?.Invoke();
        public void InvokeOnFightStarted() => OnFightStarted?.Invoke();
        public void InvokeOnFightEnd() => OnFightEnd?.Invoke();
    }
}