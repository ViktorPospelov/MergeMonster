using Infrastructure;
using UnityEngine;

public class EnemyUnitGrid : UnitGrid
{
    public EnemyUnitGrid(Game game) : base(game)
    {
    }    

    protected override int[] GetUnitsData() => _game.GetEnemyUnitsData();
    protected override GridSlot GetSlotsPrefab() => _game.EnemyGridSlotPrefab;

    protected override void SetParentPointPosition()
    {
        _parentTransform.localScale = new Vector2(-1, 1);
        _parentTransform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 10f));
    }
}
