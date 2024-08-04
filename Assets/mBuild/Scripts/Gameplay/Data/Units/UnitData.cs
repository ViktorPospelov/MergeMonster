using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(menuName = "Units data/new Unit Data")]
    public class UnitData : ScriptableObject
    {
        [field: SerializeField] public int MaxHealth { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }

    }
}