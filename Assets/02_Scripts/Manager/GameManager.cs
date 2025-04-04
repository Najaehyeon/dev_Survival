using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [field : SerializeField] public int Score { get; private set; }
    [field: SerializeField] public int Money { get; private set; }
    [field: SerializeField] public int Day { get; private set; } = 1;
    [field: SerializeField] public float PassedTime { get; private set; }
    [field: SerializeField] public bool isMissionInProgress;

    [field: SerializeField] public int Stress { get; private set; }

    public GameStateMachine stateMachine;

    public void Init(int money, int day, int stress)
    {
        Money = money;
        Day = day;
        Stress = stress;
        UIManager.Instance.ChangeStatusUI(Status.Day, Day);
        UIManager.Instance.ChangeStatusUI(Status.Money, Money);
        UIManager.Instance.ChangeStatusUI(Status.Stress, stress);
    }
    private void OnEnable()
    {
        DataManager.Instance.LoadGameManager();
    }
    private void Start()
    {
        stateMachine = gameObject.AddComponent<GameStateMachine>();
        stateMachine.Init();
        UIManager.Instance.ChangeStatusUI(Status.Money, Money);
        UIManager.Instance.ChangeStatusUI(Status.Day, Day);
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
        UIManager.Instance.ChangeStatusUI(Status.Day, Day);
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
        UIManager.Instance.ChangeStatusUI(Status.Score, Score);
    }

    /// <summary>
    /// Money에 변동이 있을 때 사용
    /// </summary>
    /// <param name="amount">+,-의 Money의 변동값</param>
    public void ChangeMoney(int amount)
    {
        Money = Mathf.Max(0, Money + amount);
        UIManager.Instance.ChangeStatusUI(Status.Money, Money);
    }

    public void ChangeStress(int amount)
    {
        Stress = Mathf.Max(0, Stress + amount);
        UIManager.Instance.ChangeStatusUI(Status.Stress, (float)Stress);
        if (Stress >= 100)
        {
            Stress = 100;
            SceneManager.LoadScene("BadEndingScene");
        }
    }

}
