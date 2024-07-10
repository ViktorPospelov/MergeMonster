namespace FSM
{
    public interface IGameState
    {
        void Enter();

        void Operate();

        void Exit();
    }
}