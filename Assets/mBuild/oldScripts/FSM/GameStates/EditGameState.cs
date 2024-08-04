using Infrastructure;
using YG;

namespace FSM
{
    public class EditGameState : IGameState
    {
        private GameStateMachine _stateMachine;

        private DragHandler _dragHandler;
        private EnemyUnitGrid _enemyGrid;
        private AllyUnitGrid _allyGrid;
        private EventBus _eventBus;

        public EditGameState(GameStateMachine stateMachine) 
        {
            _stateMachine = stateMachine;

            _dragHandler = ServiceLocator.GetService<DragHandler>();
            _enemyGrid = ServiceLocator.GetService<EnemyUnitGrid>();
            _allyGrid = ServiceLocator.GetService<AllyUnitGrid>();
            _eventBus = ServiceLocator.GetService<EventBus>();
        }

        public void Enter()
        {
            _allyGrid.OnGridStateChanged += SaveUnitData;
            _eventBus.OnFightStartButtonDown += StartFight;

            _allyGrid.FillGrid();
            _enemyGrid.FillGrid();
        }

        public void Exit()
        {   
            _allyGrid.OnGridStateChanged -= SaveUnitData;
            _eventBus.OnFightStartButtonDown -= StartFight;
        }

        public void Operate()
        {
            _dragHandler.Operate();
        }

        private void SaveUnitData()
        {
            YandexGame.savesData.playerUnits = _allyGrid.GetLevelsData();
            YandexGame.SaveProgress();
        }

        private void StartFight()
        {
            _stateMachine.GoTo<FightGameState>();
        }
    }
}