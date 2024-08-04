using Infrastructure;
using UnityEngine.AI;

namespace FSM
{
    public class FightGameState : IGameState
    {
        private GameStateMachine _stateMachine;
        private EventBus _eventBus;

        public FightGameState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            //_game.AllyUnitGrid.
            //_game.EnemyUnitGrid.
            _eventBus.OnFightEnd += EndFight;
        }

        public void Exit()
        {
            _eventBus.OnFightEnd -= EndFight;
        }

        public void Operate()
        {
        }

        private void EndFight()
        {
            _stateMachine.GoTo<EditGameState>();
        }
    }
}