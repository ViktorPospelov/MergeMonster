public class AllyGridSlot : GridSlot
{
    public override void SetUnit(Unit unit)
    {
        base.SetUnit(unit);
        Unit.gameObject.AddComponent<DragableObject>();
    }
}
