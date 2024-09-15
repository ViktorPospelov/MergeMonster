using Scripts.Gameplay.InputSystem;
using Scripts.Infrastructure;
using UnityEngine;

namespace FSM
{
    public class BootstrapGameState : IGameState
    {
        private GameStateMachine _stateMachine;
        
        public BootstrapGameState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            InitializeServices();
            _stateMachine.GoTo<EditGameState>();
        }

        public void Operate()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                ServiceLocator.GetService<AllyUnitGrid>().AddNewUnit();
                Debug.Log("add new unit");
            }
            ServiceLocator.GetService<DragHandler>().Operate();
        }

        public void Exit()
        {
            
        }

        private void InitializeServices()
        {
            ServiceLocator.GetService<AllyUnitGrid>().FillGrid();
            ServiceLocator.GetService<EnemyUnitGrid>().FillGrid();
        }
    }
}