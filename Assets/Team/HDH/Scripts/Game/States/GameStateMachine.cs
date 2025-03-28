public class GameStateMachine : BaseStateMachine
{
    public InGameState inGameState;
    public ScoreState scoreState;
    public ShopState shopState;

    public override void Init()
    {
        inGameState = new InGameState(this);
        scoreState = new ScoreState(this);
        shopState = new ShopState(this);

        ChangeState(inGameState);
    }
}
