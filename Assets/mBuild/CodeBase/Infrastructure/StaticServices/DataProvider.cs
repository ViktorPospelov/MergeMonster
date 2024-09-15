using Gameplay.GridSystem;
using Gameplay.Data;
using UnityEngine;

public class DataProvider : MonoBehaviour
{
    [field: SerializeField] public EnemyGridSlot EnemyGridSlotPrefab { get; private set; }
    [field: SerializeField] public AllyGridSlot AllyGridSlotPrefab { get; private set; }

    [field: SerializeField] public Unit UnitPrefab { get; private set; }

    [field: SerializeField] public UnitDataContainer UnitDataContainer { get; private set; }

    [field: SerializeField] public LevelContainer LevelContainer { get; private set; }
}
