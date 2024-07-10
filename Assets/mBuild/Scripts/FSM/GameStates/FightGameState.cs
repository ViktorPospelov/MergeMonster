using Infrastructure;
using UnityEngine.AI;

namespace FSM
{
    public class FightGameState : IGameState
    {
        private Game _game;
        private GameStateMachine _stateMachine;

        public FightGameState(Game game, GameStateMachine stateMachine)
        {
            _game = game;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            //_game.AllyUnitGrid.
            //_game.EnemyUnitGrid.
            _game.ReadOnlyEventBus.OnFightEnd += EndFight;
        }

        public void Exit()
        {
            _game.ReadOnlyEventBus.OnFightEnd -= EndFight;
        }

        public void Operate()
        {
            if (_game.AllyUnitGrid.GetUnitsCount() == 0)
            {
                //lose popup
                EndFight();
            }
            else if (_game.EnemyUnitGrid.GetUnitsCount() == 0)
            {
                //win popup
                //opened levels++
                EndFight();
            }
        }

        private void EndFight()
        {
            _stateMachine.GoTo<EditGameState>();
        }
    }
}