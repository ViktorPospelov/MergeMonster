using UnityEngine;

public abstract class GridSlot : MonoBehaviour 
{
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
        Unit = unit;
        Unit.transform.parent = transform;
        unit.transform.localPosition = Vector2.zero;
    }

    public void Clear(bool destroyUnit = false)
    {
        if (Unit != null && destroyUnit)
            Destroy(Unit.gameObject);

        Unit = null;
    }
}
