using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [field : SerializeField] public int Score { get; private set; }
    [field: SerializeField] public int Money { get; private set; }
    [field: SerializeField] public int Day { get; private set; }
    [field: SerializeField] public float PassedTime { get; private set; }
    [field: SerializeField] public bool isMissionInProgress;
    //MissionTimer 에서 변경

    //UI test를 위해 추가 추후 적용시 삭제
    [SerializeField] public UItest uiTest;

    public GameStateMachine stateMachine;

    private void Start()
    {
        stateMachine = gameObject.AddComponent<GameStateMachine>();
        stateMachine.Init();

        //uiTest.ScoreText.text = Score.ToString();
        //uiTest.MoneyText.text = Money.ToString();
    }

    private void Update()
    {
        stateMachine.StateUpdate();
    }

    /// <summary>
    /// Day에 1을 더함
    /// </summary>
    public void PassDay()
    {
        Day++;
    }

    /// <summary>
    /// 입력한 시간만큼 PassedTime에 더함
    /// </summary>
    /// <param name="time">더할 시간 입력</param>
    public void PassTime(float time)
    {
        PassedTime += time;
    }
    /// <summary>
    /// 시간을 0으로 초기화
    /// </summary>
    public void SetTimeZero()
    {
        PassedTime = 0f;
    }

    /// <summary>
    /// Score에 변동이 있을 때 사용
    /// </summary>
    /// <param name="amount">+,-의 점수에 변동 값</param>
    public void ChangeScore(int amount)
    {
        Score = Mathf.Max(0, Score + amount);
        //uiTest.ScoreText.text = Score.ToString();
    }

    /// <summary>
    /// Money에 변동이 있을 때 사용
    /// </summary>
    /// <param name="amount">+,-의 Money의 변동값</param>
    public void ChangeMoney(int amount)
    {
        Money = Mathf.Max(0, Money + amount);
        //uiTest.MoneyText.text = Money.ToString();
    }

}
