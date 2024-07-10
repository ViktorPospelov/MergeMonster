using Infrastructure;
using UnityEngine;

public abstract class UnitGrid
{
    protected GridSlot[,] _slots = new GridSlot[3, 5];

    protected Transform _parentTransform = new GameObject("UnitGrid").transform;
    protected Game _game;

    public UnitGrid(Game game)
    {
        _game = game;

        CreateGrid();
        FillGrid(GetUnitsData());
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
    public void FillGrid(int[] units)
    {
        for (int y = 0; y < _slots.GetLength(0); y++)
        {
            for (int x = 0; x < _slots.GetLength(1); x++)
            {
                if (units[x + (y * _slots.GetLength(1))] != 0)
                {
                    Unit unit = MonoBehaviour.Instantiate(_game.UnitPrefab);
                    unit.Initialize(units[x + (y * _slots.GetLength(1))]);
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


    protected abstract int[] GetUnitsData();
    protected abstract GridSlot GetSlotsPrefab();
    protected abstract void SetParentPointPosition();    
}

