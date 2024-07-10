using Array2DEditor;
using UnityEngine;

namespace Utilites.LevelCreate
{
    [CreateAssetMenu(menuName = "Level Settings/New level", fileName = "Level X")]
    public class LevelInfo : ScriptableObject
    {
        [field: SerializeField] public bool IsBossLevel;

        [field: SerializeField] public Array2DInt EnemyLevels;
        [field: SerializeField] public int BossLevel;
    }
}
