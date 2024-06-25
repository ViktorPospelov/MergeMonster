using System.Collections;
using UnityEngine;

public class UnitGrid : MonoBehaviour
{
    [SerializeField] private UnitGridSlot[,] _slots = new UnitGridSlot[5, 3];

    [SerializeField] private UnitGridSlot _gridSlotPrefab;
    [SerializeField] private Unit[] _startUnits;
    [SerializeField] private LayerMask _slotsLayer;

    private void Start()
    {
        InitArray();
        for (int i = 0; i < _startUnits.Length; i++)
        {
            _slots[i, 0].SetUnit(_startUnits[i]);
        }
    }

    public void TryTakeSlot(UnitGridSlot newSlot, Unit unit)
    {
        UnitGridSlot oldSlot = null;

        foreach (UnitGridSlot slot in _slots)
            if (slot.Unit == unit) oldSlot = slot;

        if (newSlot.Unit == null || newSlot == oldSlot)
        {
            print(1);
            oldSlot.Clear();
            newSlot.SetUnit(unit);
        }
        else if (newSlot.Unit != null) 
        {
            if (newSlot.Unit.Level != unit.Level)
            {
                print(2);
                var tempUnit = newSlot.Unit;

                newSlot.SetUnit(unit);
                oldSlot.SetUnit(tempUnit);
            }
            else
            {
                print(3);
                oldSlot.Clear(true);
                newSlot.Unit.IncreaseLevel();
            }
        }
    }

    private void InitArray()
    {
        int x = _slots.GetUpperBound(0) + 1;
        int y = _slots.GetUpperBound(1) + 1;
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                _slots[j, i] = Instantiate(_gridSlotPrefab);
                _slots[j, i].Init(this, j + (i * x));
                _slots[j, i].transform.localPosition = new Vector2((float)j + 0.5f, (float)i - 1f);
                
            }
        }
    }
}
