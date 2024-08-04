using Scripts.Gameplay.GridSystem;
using Infrastructure;
using UnityEngine;

public class EnemyUnitGrid : UnitGrid
{
    protected override int[] GetUnitsData() => ServiceLocator.GetService<Bootstrap>().GetEnemyUnitsData();
    protected override GridSlot GetSlotsPrefab() => ServiceLocator.GetService<Bootstrap>().EnemyGridSlotPrefab;

    protected override void SetParentPointPosition()
    {
        _parentTransform.localScale = new Vector2(-1, 1);
        _parentTransform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 10f));
    }

    protected override Team GetTeam() => Team.Enemy;
}
