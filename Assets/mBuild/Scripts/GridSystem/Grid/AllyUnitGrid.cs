using Infrastructure;
using UnityEngine;
using System;

public class AllyUnitGrid : UnitGrid
{
    public event Action OnGridStateChanged;

    public AllyUnitGrid(Game game) : base(game)
    {
        
    }

    public int[] GetLevelsData()
    {
        int[] data = new int[15];

        for (int y = 0; y < _slots.GetLength(0); y++)
        {
            for (int x = 0; x < _slots.GetLength(1); x++)
            {
                if (_slots[y, x].Unit != null)
                    data[x + (y * x)] = _slots[y, x].Unit.Level;
                else
                    data[x + (y * x)] = 0;
            }
        }
        return data;
    }

    public void TryTakeSlot(GridSlot newSlot, Unit unit)
    {
        GridSlot oldSlot = null;

        foreach (GridSlot slot in _slots)
            if (slot.Unit == unit) oldSlot = slot;

        if (newSlot.Unit == null || newSlot == oldSlot)
        {
            Debug.Log(1);
            oldSlot.Clear();
            newSlot.SetUnit(unit);
        }
        else if (newSlot.Unit != null)
        {
            if (newSlot.Unit.Level != unit.Level)
            {
                Debug.Log(2);
                var tempUnit = newSlot.Unit;

                newSlot.SetUnit(unit);
                oldSlot.SetUnit(tempUnit);
            }
            else
            {
                Debug.Log(3);
                oldSlot.Clear(true);
                newSlot.Unit.IncreaseLevel();
            }
        }

        OnGridStateChanged?.Invoke();
    }

    public void AddNewUnit(int unitLevel = 1)
    {
        if (TryFindFreeSlot(out GridSlot slot))
        {
            Unit unit = MonoBehaviour.Instantiate(_game.UnitPrefab);
            unit.Initialize(unitLevel);
            slot.SetUnit(unit);

            OnGridStateChanged?.Invoke();
        }
    }

    private bool TryFindFreeSlot(out GridSlot slot)
    {
        for (int y = 0; y < _slots.GetLength(0); y++)
        {
            for (int x = 0; x < _slots.GetLength(1); x++)
            {
                if (_slots[y, x].Unit == null)
                {
                    slot = _slots[y, x];
                    return true;
                }
            }
        }
        slot = null;
        return false;
    }

    protected override void SetParentPointPosition() 
        => _parentTransform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, 10f));

    protected override int[] GetUnitsData() => _game.GetPlayerUnitsData();

    protected override GridSlot GetSlotsPrefab() => _game.AllyGridSlotPrefab;
}
