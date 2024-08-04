
public class Game 
{
    private GameFieldMediator _gameFieldHandler;
    private UIMediator _uiHandler;

    public Game(GameFieldMediator gameFieldHandler,UIMediator uiMediator)
    {
        _gameFieldHandler = gameFieldHandler;
        _uiHandler = uiMediator; 

        OnGameStart();
    }

    private void OnGameStart()
    {
        
    }

    private void OnFightStart()
    {

    }

    private void OnFightEnd()
    {

    }
}
