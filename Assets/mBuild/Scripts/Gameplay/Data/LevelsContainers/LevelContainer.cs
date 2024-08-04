using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(menuName = "Levels data/new Levels container")]
    public class LevelContainer : ScriptableObject
    {
        [field: SerializeField] public LevelInfo[] levelsData;
    }
}