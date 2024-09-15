using Scripts.Gameplay.InputSystem;
using System.Collections;
using UnityEngine;

namespace Scripts.Infrastructure
{
    public class GameFieldMediator
    {
        private EnemyUnitGrid _enemyUnitGrid;
        private AllyUnitGrid _allyUnitGrid;
        private DragHandler _dragHandler;

        public GameFieldMediator()
        {
            _allyUnitGrid = new AllyUnitGrid();
            //_allyUnitGrid.FillGrid();
            ServiceLocator.AddService(_allyUnitGrid);

            _enemyUnitGrid = new EnemyUnitGrid();
            //_enemyUnitGrid.FillGrid();
            ServiceLocator.AddService(_enemyUnitGrid);

            _dragHandler = new DragHandler();
            ServiceLocator.AddService(_dragHandler);
        }

        private void OnStart()
        {
        
        }

        public void Update()
        {
            _dragHandler.Operate();

            if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("Ally count: " + _allyUnitGrid.GetUnitsCount());
                Debug.Log("Enemy count: " + _enemyUnitGrid.GetUnitsCount());
            }
        }

        public void StartFight()
        {
            _allyUnitGrid.OnAllUnitDie += StopFight;
            _enemyUnitGrid.OnAllUnitDie += StopFight;

            Corutine.Instance.StartCoroutine(StartFightRoutine());              
        }

        private void StopFight(UnitGrid unitGrid)
        {
            _allyUnitGrid.OnAllUnitDie -= StopFight;
            _enemyUnitGrid.OnAllUnitDie -= StopFight;

            if (unitGrid == _allyUnitGrid)
            {
                Debug.Log("lose");
            }
            else if (unitGrid == _enemyUnitGrid)
            {
                Debug.Log("win");
            }

            //_allyUnitGrid.FillGrid();
            //_enemyUnitGrid.FillGrid();
        }

        private IEnumerator StartFightRoutine()
        {
            _allyUnitGrid.StartFight(_enemyUnitGrid);

            yield return new WaitForSeconds(0.5f);

            _enemyUnitGrid.StartFight(_allyUnitGrid);
        }
    }
}
