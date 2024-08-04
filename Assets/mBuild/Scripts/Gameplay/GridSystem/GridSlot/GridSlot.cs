using System;
using UnityEngine;

public abstract class GridSlot : MonoBehaviour 
{
    public event Action OnUnitDie;

    public UnitGrid UnitGrid { get; protected set; }
    public Unit Unit { get; private set; }

    public void Init(UnitGrid unitGrid, Transform parent, int slotNumber)
    {
        UnitGrid = unitGrid;
        transform.parent = parent;

        if (slotNumber % 2 == 0)
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.5f);
        else
            GetComponent<SpriteRenderer>().color = Color.clear;
    }

    public virtual void SetUnit(Unit unit)
    {
        if (Unit != null)
            Unit.OnDie -= InvokeOnUnitDie;

        Unit = unit;
        Unit.transform.parent = transform;
        Unit.transform.localPosition = Vector2.zero;

        if (Unit != null)
            Unit.OnDie += InvokeOnUnitDie;
    }

    public void Clear(bool destroyUnit = false)
    {
        if (Unit != null)
        {
            Unit.OnDie -= InvokeOnUnitDie;

            if (destroyUnit)
                Destroy(Unit.gameObject);

            Unit = null;
        }
    }

    private void InvokeOnUnitDie()
    {
        Unit = null;
        OnUnitDie?.Invoke();
    }
}
