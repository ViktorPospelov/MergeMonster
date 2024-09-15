using Scripts.Infrastructure;
using UnityEngine;
using System;
using Gameplay.GridSystem;

public abstract class UnitGrid
{
    public event Action<UnitGrid> OnAllUnitDie;

    protected readonly Unit[,] Units = new Unit[3, 5];

    protected readonly Transform ParentTransform = new GameObject("UnitGrid").transform;
    protected Bootstrap _game;

    private Unit _unitPrefab;
    private bool _isFighting;

    public UnitGrid()
    {
        _game = ServiceLocator.GetService<Bootstrap>();
        _unitPrefab = ServiceLocator.GetService<DataProvider>().UnitPrefab;
        CreateGrid();
    }

    public void StartFight(UnitGrid targetGrid)
    {
        _isFighting = true;
        foreach (var unit in Units) unit.StartFight(targetGrid);
    }

    public Unit GetNearestUnit(Unit enemyUnit)
    {
        if (enemyUnit == null)
            throw new NullReferenceException();

        Unit result = null;
        float minDistance = float.MaxValue;

        foreach (var unit in Units)
        {
            if (unit.State != UnitState.Disabled)
            {
                if (Vector2.Distance(unit.transform.position, unit.transform.position) <= minDistance)
                {
                    minDistance = Vector2.Distance(unit.transform.position, unit.transform.position);
                    result = unit;
                }
            }
        }

        return result;
    }

    public int GetUnitsCount()
    {
        int count = 0;
        foreach (Unit unit in Units)
            if (unit.State != UnitState.Disabled) count++;

        return count;
    }

    public void FillGrid()
    {
        int[] data = GetUnitsData();

        for (int y = 0; y < Units.GetLength(0); y++)
        {
            for (int x = 0; x < Units.GetLength(1); x++)
            {
                Units[y, x].Initialize(data[x + (y * Units.GetLength(1))], GetTeam());
            }
        }
    }

    private void CreateGrid()
    {
        SetParentObjectPosition();

        for (int y = 0; y < Units.GetLength(0); y++)
        {
            for (int x = 0; x < Units.GetLength(1); x++)
            {
                Units[y, x] = MonoBehaviour.Instantiate(_unitPrefab);
                Units[y, x].transform.parent = ParentTransform;
                Units[y, x].transform.localPosition = new Vector2((float)x + 0.5f, (float)y - 1f);
                Units[y, x].Initialize(0, GetTeam());
                Units[y, x].OnDie += CheckUnitsCount;
            }
        }
    }

    private void CheckUnitsCount()
    {
        Debug.Log(GetUnitsCount() + " _____ " + GetTeam());
        if (_isFighting && GetUnitsCount() <= 0)
        {
            OnAllUnitDie?.Invoke(this);
            _isFighting = false;
        }
    }

    protected abstract void SetParentObjectPosition();
    protected abstract int[] GetUnitsData();
    protected abstract Team GetTeam();
}

