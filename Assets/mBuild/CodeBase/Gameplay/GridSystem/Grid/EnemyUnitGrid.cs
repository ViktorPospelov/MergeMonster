using Gameplay.GridSystem;
using Scripts.Infrastructure;
using UnityEngine;

public class EnemyUnitGrid : UnitGrid
{
    protected override int[] GetUnitsData()
    {
        int[] tempArray = new int[15];
        var levelData = ServiceLocator.GetService<DataProvider>().LevelContainer.levelsData[PlayerData.CurrentLevel];

        if (levelData.IsBossLevel)
        {
            //boss level generation
            return new int[15];
        }

        else
        {
            var unitArray = levelData.EnemyLevels;
            
            for(int i = 0; i < unitArray.GridSize.y; i++)
            for(int j = 0; j < unitArray.GridSize.x; j++) 
                tempArray[(tempArray.Length - 1) - (j + i * unitArray.GridSize.x)] 
                    = unitArray.GetCell(j, i);
        
            return tempArray;
        }
    }

    protected override Team GetTeam() => Team.Enemy;

    protected override void SetParentObjectPosition()
    {
        ParentTransform.localScale = new Vector2(-1, 1);
        ParentTransform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 10f));
    }
}
