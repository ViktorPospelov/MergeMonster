using Infrastructure;
using UnityEngine;
using System;
using Scripts.Gameplay.GridSystem;

public abstract class UnitGrid
{
    public event Action<UnitGrid> OnAllUnitDie;

    protected GridSlot[,] _slots = new GridSlot[3, 5];

    protected Transform _parentTransform = new GameObject("UnitGrid").transform;
    protected Bootstrap _game;

    public UnitGrid()
    {
        _game = ServiceLocator.GetService<Bootstrap>();
        CreateGrid();
    }

    public void StartFight(UnitGrid targetGrid)
    {
        foreach (var slot in _slots) 
        {
            slot.OnUnitDie += CheckUnitsCount;

            if (slot.Unit != null)
                slot.Unit.Attack(targetGrid);
        }
    }

    public Unit GetNearestUnit(Unit unit)
    {
        if (unit == null)
            throw new NullReferenceException();

        Unit result = null;
        float minDistance = float.MaxValue;

        foreach (var slot in _slots)
        {
            if (slot.Unit != null)
            {
                if (Vector2.Distance(unit.transform.position, slot.Unit.transform.position) <= minDistance)
                {
                    result = slot.Unit;
                    minDistance = Vector2.Distance(unit.transform.position, slot.Unit.transform.position);
                }
            }
        }

        return result;
    }

    public int GetUnitsCount()
    {
        int count = 0;
        foreach (GridSlot slot in _slots) 
        {
            if (slot.Unit != null) count++;
        }

        return count;
    }

    public void FillGrid()
    {
        int[] units = GetUnitsData();

        for (int y = 0; y < _slots.GetLength(0); y++)
        {
            for (int x = 0; x < _slots.GetLength(1); x++)
            {
                if (units[x + (y * _slots.GetLength(1))] != 0)
                {
                    Unit unit = MonoBehaviour.Instantiate(ServiceLocator.GetService<Bootstrap>().UnitPrefab);
                    unit.Initialize(units[x + (y * _slots.GetLength(1))], GetTeam());
                    _slots[y, x].SetUnit(unit);
                }
            }
        }
    }

    private void CreateGrid()
    {
        SetParentPointPosition();

        for (int y = 0; y < _slots.GetLength(0); y++)
        {
            for (int x = 0; x < _slots.GetLength(1); x++)
            {
                _slots[y, x] = MonoBehaviour.Instantiate(GetSlotsPrefab());
                _slots[y, x].Init(this, _parentTransform, x + (y * _slots.GetLength(1)));
                _slots[y, x].transform.localPosition = new Vector2((float)x + 0.5f, (float)y - 1f);
            }
        }
    }

    private void CheckUnitsCount()
    {
        Debug.Log(GetUnitsCount() + " _____ " + GetTeam());
        if (GetUnitsCount() <= 0)
        {
            OnAllUnitDie?.Invoke(this);
            foreach(var slot in _slots)
            {
                slot.OnUnitDie -= CheckUnitsCount;
            }
        }
    }

    protected abstract Team GetTeam();
    protected abstract int[] GetUnitsData();
    protected abstract GridSlot GetSlotsPrefab();
    protected abstract void SetParentPointPosition();
}

