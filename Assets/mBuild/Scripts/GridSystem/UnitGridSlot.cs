using UnityEngine;

public class UnitGridSlot : MonoBehaviour
{
    public UnitGrid UnitGrid { get; private set; }
    [field: SerializeField] public Unit Unit { get; private set; }

    public void Init(UnitGrid unitGrid, int slotNumber)
    {
        UnitGrid = unitGrid;
        transform.parent = unitGrid.transform;

        if (slotNumber % 2 == 0)
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.5f);
        else
            GetComponent<SpriteRenderer>().color = Color.clear;
    }

    public void SetUnit(Unit unit)
    {
        Unit = unit;
        Unit.transform.parent = transform;
        unit.transform.localPosition = Vector2.zero;
    }

    public void Clear(bool destroy = false)
    {
        if (Unit != null && destroy)
            Destroy(Unit.gameObject);

        Unit = null;
    }
}
