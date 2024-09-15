using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Data
{
    [CreateAssetMenu(menuName = "Units data/new Unit Data Container")]
    public class UnitDataContainer : ScriptableObject
    {
        [FormerlySerializedAs("unitsDataContainer")] [field: SerializeField] public UnitData[] unitsData; 
    }
}