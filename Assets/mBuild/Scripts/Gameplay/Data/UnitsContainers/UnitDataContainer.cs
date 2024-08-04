using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(menuName = "Units data/new Unit Data Container")]
    public class UnitDataContainer : ScriptableObject
    {
        [field: SerializeField] public UnitData[] unitsDataContainer;
    }
}