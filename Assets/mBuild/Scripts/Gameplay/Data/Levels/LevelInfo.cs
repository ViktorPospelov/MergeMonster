using Array2DEditor;
using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(menuName = "Levels data/new level", fileName = "Level X")]
    public class LevelInfo : ScriptableObject
    {
        [field: SerializeField] public bool IsBossLevel;

        [field: SerializeField] public Array2DInt EnemyLevels;
        [field: SerializeField] public int BossLevel;
    }
}
