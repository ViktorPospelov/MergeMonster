using Infrastructure;
using UnityEngine;
using YG;

namespace FSM
{
    public class EditGameState : IGameState
    {
        private GameStateMachine _stateMachine;
        private Game _game;

        public EditGameState(Game game, GameStateMachine stateMachine) 
        {
            _game = game;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _game.AllyUnitGrid.OnGridStateChanged += SaveUnitData;
            _game.ReadOnlyEventBus.OnFightStartButtonDown += StartFight;

            _game.AllyUnitGrid.FillGrid(_game.GetPlayerUnitsData());
            _game.EnemyUnitGrid.FillGrid(_game.GetEnemyUnitsData());
        }

        public void Exit()
        {   
            _game.AllyUnitGrid.OnGridStateChanged -= SaveUnitData;
            _game.ReadOnlyEventBus.OnFightStartButtonDown -= StartFight;
        }

        public void Operate()
        {
            _game.DragHandler.Operate();
        }

        private void SaveUnitData()
        {
            YandexGame.savesData.playerUnits = _game.AllyUnitGrid.GetLevelsData();
            YandexGame.SaveProgress();
        }

        private void StartFight()
        {
            _stateMachine.GoTo<FightGameState>();
        }
    }
}