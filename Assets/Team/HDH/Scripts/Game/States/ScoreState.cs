using UnityEngine;

public enum ScoreRate
{
    Bad,
    Normal,
    Good,
    Prefect
}

public class ScoreState : GameBaseState
{
    public int[] thresholdScores = new int[]{ 50, 70, 100 };

    public ScoreState(GameStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        //점수 결과 창 표시
        ShowResult(RateScore());
    }

    public override void Exit()
    {
        ScoreToMoney();
    }

    public override void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //stateMachine.ChangeState(stateMachine.shopState);
        }
    }

    void ShowResult(ScoreRate rate)
    {
        switch(rate)
        {
            case ScoreRate.Bad:
                //UI 요소 활성화
                break;
            case ScoreRate.Normal:
                //UI 요소 활성화
                break;
            case ScoreRate.Good:
                //UI 요소 활성화
                break;
            case ScoreRate.Prefect:
                //UI 요소 활성화
                break;
        }
    }

    ScoreRate RateScore()
    {
        if (GameManager.Instance.Score < thresholdScores[0])
            return ScoreRate.Bad;
        else if (GameManager.Instance.Score < thresholdScores[1])
            return ScoreRate.Normal;
        else if (GameManager.Instance.Score < thresholdScores[2])
            return ScoreRate.Good;
        else
            return ScoreRate.Prefect;
    }

    void ScoreToMoney()
    {
        GameManager.Instance.ChangeMoney(GameManager.Instance.Score);
        GameManager.Instance.ChangeScore(-GameManager.Instance.Score);
    }

}
