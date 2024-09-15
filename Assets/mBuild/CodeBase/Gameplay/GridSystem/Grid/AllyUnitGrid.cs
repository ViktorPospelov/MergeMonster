using Gameplay.GridSystem;
using Scripts.Infrastructure;
using UnityEngine;
using System;
using YG;

public class AllyUnitGrid : UnitGrid
{
    public event Action OnGridStateChanged;

    public int[] GetLevelsData()
    {
        int[] data = new int[15];

        for (int y = 0; y < Units.GetLength(0); y++)
        {
            for (int x = 0; x < Units.GetLength(1); x++)
            {
                data[x + (y * x)] = Units[y, x].Level;
            }
        }
        return data;
    }

    public void TryTakeSlot(Unit firstUnit, Unit secondUnit)
    {
        /*GridSlot oldSlot = null;

        foreach (GridSlot slot in Units)
            if (slot.Unit == movedUnit) oldSlot = slot;

        if (newSlot.Unit == null || newSlot == oldSlot)
        {
            Debug.Log(1);
            oldSlot.Clear();
            newSlot.SetUnit(movedUnit);
        }
        else if (newSlot.Unit != null)
        {
            if (newSlot.Unit.Level != movedUnit.Level)
            {
                Debug.Log(2);
                var tempUnit = newSlot.Unit;

                newSlot.SetUnit(movedUnit);
                oldSlot.SetUnit(tempUnit);
            }
            else
            {
                Debug.Log(3);
                oldSlot.Clear(true);
                newSlot.Unit.IncreaseLevel();
            }
        }*/
        
        //IncreaseLevel
        if (firstUnit.Level == 0)
            return;
        
        if (firstUnit.Level == secondUnit.Level)
        {
            firstUnit.Initialize(0, Team.Ally);
            secondUnit.IncreaseLevel();
        }
        //Swap
        else
        {
            var tempLevel = firstUnit.Level;
            firstUnit.Initialize(secondUnit.Level, Team.Ally);
            secondUnit.Initialize(tempLevel, Team.Ally);
        }
        
        OnGridStateChanged?.Invoke();
    }

    public void AddNewUnit(int unitLevel = 1)
    {
        if (TryFindFreeSlot(out Unit freeUnit))
        {
            freeUnit.Initialize(unitLevel, Team.Ally);

            OnGridStateChanged?.Invoke();
        }
    }

    private bool TryFindFreeSlot(out Unit freeUnit)
    {
        for (int y = 0; y < Units.GetLength(0); y++)
        {
            for (int x = 0; x < Units.GetLength(1); x++)
            {
                if (Units[y, x].Level == 0)
                {
                    freeUnit = Units[y, x];
                    return true;
                }
            }
        }
        freeUnit = null;
        return false;
    }

    protected override void SetParentObjectPosition() 
        => ParentTransform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, 10f));
    

    protected override int[] GetUnitsData() => YandexGame.savesData.playerUnits;

    protected override Team GetTeam() => Team.Ally;
}
