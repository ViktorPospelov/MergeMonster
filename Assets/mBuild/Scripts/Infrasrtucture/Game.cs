using UnityEngine;
using FSM;
using YG;
using Array2DEditor;

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
    public class Game : MonoBehaviour
    {

        public EnemyUnitGrid EnemyUnitGrid { get; private set; }
        public AllyUnitGrid AllyUnitGrid { get; private set; }
        public DragHandler DragHandler { get; private set; }
        public IReadOnlyEventBus ReadOnlyEventBus => _eventBus;

        //prefabs
        [field: SerializeField] public EnemyGridSlot EnemyGridSlotPrefab { get; private set; }
        [field: SerializeField] public AllyGridSlot AllyGridSlotPrefab { get; private set; }
        [field: SerializeField] public Unit UnitPrefab { get; private set; }

        private GameStateMachine _stateMachine;
        private EventBus _eventBus;

        [SerializeField] private Array2DInt ints;
        private int[] _testArray;

        private void Start()
        {
            _testArray = new int[ints.GridSize.y * ints.GridSize.x];

            for (int y = 0; y < ints.GridSize.y; y++)
            {
                for (int x = 0; x < ints.GridSize.x; x++)
                {
                    _testArray[_testArray.Length - (1 + x + (y * ints.GridSize.x))] = ints.GetCell(x, y);
                }
            }

            InitEventBus();
            InitDragHandler();
            InitAllyUnitGrid();
            InitEnemyUnitGrid();

            InitStateMachine();
        }

        private void Update() => _stateMachine.UseActiveState();

        public int[] GetPlayerUnitsData() => YandexGame.savesData.playerUnits;
        public int[] GetEnemyUnitsData() => _testArray;

        private void InitDragHandler()
        {
            DragHandler = new DragHandler(this);
        }

        private void InitEventBus()
        {
            _eventBus = new EventBus();
        }

        private void InitAllyUnitGrid()
        {
            AllyUnitGrid = new AllyUnitGrid(this);
        }

        private void InitEnemyUnitGrid()
        {
            EnemyUnitGrid = new EnemyUnitGrid(this);
        }

        private void InitStateMachine()
        {
            _stateMachine = new GameStateMachine();
            _stateMachine.AddState(new EditGameState(this, _stateMachine));
            _stateMachine.AddState(new FightGameState(this, _stateMachine));
        }
    }
}

