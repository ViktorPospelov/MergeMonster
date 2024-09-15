using Gameplay.GridSystem;
using Array2DEditor;
using UnityEngine;
using FSM;
using UnityEngine.Serialization;

namespace Scripts.Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        [field: SerializeField] public EnemyGridSlot EnemyGridSlotPrefab { get; private set; }
        [field: SerializeField] public AllyGridSlot AllyGridSlotPrefab { get; private set; }
        [field: SerializeField] public Unit UnitPrefab { get; private set; }
        
        [Header("Game Components")]
        [SerializeField] private DataProvider _dataProvider;
        [SerializeField] private UIMediator _uiMediator;
        
        [SerializeField] private Array2DInt _testEnemyUnits;
        private int[] _tempArray;

        private GameStateMachine _stateMachine;

        private void Awake()
        {
            _tempArray = new int[15];
            for(int i = 0; i < _testEnemyUnits.GridSize.y; i++)
                for(int j = 0; j < _testEnemyUnits.GridSize.x; j++) 
                    _tempArray[(_tempArray.Length - 1) - (j + i * _testEnemyUnits.GridSize.x)] 
                        = _testEnemyUnits.GetCell(j, i);
            

            RegistryServices();
            InitStateMachine();
        }

        private void Update()
        {
            _stateMachine.Update();
        }
        
        private void RegistryServices()
        {
            ServiceLocator.AddService(this);
            ServiceLocator.AddService(_uiMediator);
            ServiceLocator.AddService(_dataProvider);

            var eventBus = new EventBus();
            ServiceLocator.AddService(eventBus);

            var gameFieldMediator = new GameFieldMediator();
            ServiceLocator.AddService(gameFieldMediator);
        }

        private void InitStateMachine()
        {
            _stateMachine = new GameStateMachine();
            
            _stateMachine.AddState(new BootstrapGameState(_stateMachine));
            _stateMachine.AddState(new FightGameState(    _stateMachine));
            _stateMachine.AddState(new EditGameState(     _stateMachine));
            
            _stateMachine.GoTo<BootstrapGameState>();
        }

        public int[] GetEnemyUnitsData() => _tempArray;
    }
}

