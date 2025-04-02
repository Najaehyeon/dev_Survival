public class GameStateMachine : BaseStateMachine
{
    public InGameState inGameState;
    public ScoreState scoreState;
    public ShopState shopState;
    public PauseState pauseState;

    public override void Init()
    {
        inGameState = new InGameState(this);
        scoreState = new ScoreState(this);
        shopState = new ShopState(this);
        pauseState = new PauseState(this);
        
        ChangeState(pauseState);
    }
}
