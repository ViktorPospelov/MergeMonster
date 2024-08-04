using Array2DEditor;
using UnityEngine;
using FSM;
using YG;
using UnityEditor.Rendering;

/// Этот скрипт служит точкой входа в игру, создает все компоненты и прокидывает зависисмости
/// -------------
/// В игре есть такие сущности, как: 
/// машина состояний, отвечает за текущее состояние игры и генерирует поведение остальных компонентов
/// --
/// союзная и вражеская сетки юнитов  
/// --
/// ДрагХендлер(перенос юнитов), 
/// --
/// шина событий для колбеков состояния игры(ивенты вызываются исключительно из машины состояний, когда дергаются уже внутренние ивенты компонентов),
/// --
/// юАйка, пока не знаю как буду ее делать, но 100% будет модель и вьюшка, модель подписывается на геймплейные ивенты из шины и реагирует на них, машина состояний не отвечает за это 
/// --
/// конктроллер сохранений(наверное, тк не знаю, юзать его или встроенный в плагин). Мне нужно сохранять: поле игрока(массив интов), монеты и текущий уровень
/// через ЯГ плагин сделаю, сложные данные тут отсутствуют
/// --
/// 
///


namespace Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        //prefabs
        [field: SerializeField] public EnemyGridSlot EnemyGridSlotPrefab { get; private set; }
        [field: SerializeField] public AllyGridSlot AllyGridSlotPrefab { get; private set; }
        [field: SerializeField] public Unit UnitPrefab { get; private set; }

        private GameStateMachine _stateMachine;

        [SerializeField] private Array2DInt ints;
        private int[] _testArray;
        private GameFieldMediator _gameFieldMediator;
        private UIMediator _uiMediator;

        private void Start()
        {
            _testArray = new int[15];
            for(int i = 0; i < ints.GridSize.y; i++)
            {
                for(int j = 0; j < ints.GridSize.x; j++)
                {
                    _testArray[(_testArray.Length - 1) - (j + i * ints.GridSize.x)] = ints.GetCell(j, i);
                }
            }
            

            CreateAllServices();
            //InitStateMachine();
        }

        private void Update()
        {
            _gameFieldMediator.Update();

            if (Input.GetKeyDown(KeyCode.R))
            {
                ServiceLocator.GetService<AllyUnitGrid>().AddNewUnit();
            }

            if (Input.GetKeyDown(KeyCode.Space))
                ServiceLocator.GetService<GameFieldMediator>().StartFight();
        }

        private void CreateAllServices()
        {
            ServiceLocator.AddService(this);

            var eventBus = new EventBus();
            ServiceLocator.AddService(eventBus);

            

            

            //var wallet = new Wallet();
            //ServiceLocator.AddService(wallet);

            _gameFieldMediator = new GameFieldMediator();
            ServiceLocator.AddService(_gameFieldMediator);

            _uiMediator = new UIMediator();
            ServiceLocator.AddService(_uiMediator);
        }

        private void InitStateMachine()
        {
            _stateMachine = new GameStateMachine();

            _stateMachine.AddState(new EditGameState( _stateMachine));
            _stateMachine.AddState(new FightGameState(_stateMachine));
        }

        public int[] GetEnemyUnitsData()
        {
            return _testArray;
        }
    }
}

