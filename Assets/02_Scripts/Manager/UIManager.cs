using UnityEngine;

public enum UIState
{
    Home,
    InGame,
    Score,
    Shop,
    Misson
}

public class UIManager : Singleton<UIManager>
{

    [Header("UI")]
    [SerializeField] public HomeUI homeUI;
    [SerializeField] public InGameUI inGameUI;
    [SerializeField] public ScoreUI scoreUI;
    [SerializeField] public ShopUI shopUI;


    private UIState currentState;


    protected void Awake()
    {

        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI.Init(this);
        inGameUI = GetComponentInChildren<InGameUI>(true);
        inGameUI.Init(this);
        scoreUI = GetComponentInChildren<ScoreUI>(true);
        scoreUI.Init(this);
        shopUI = GetComponentInChildren<ShopUI>(true);
        shopUI.Init(this);
    }
    private void Start()
    {
        ChangeState(UIState.Home);
    }

    public void ChangeStatusUI(Status status, int value)
    {
        inGameUI.ChangeStatus(status, value);
    }
    public void ChangeStatusUI(Status status, float value)
    {
        inGameUI.ChangeStatus(status, value);
    }
    public void ChangeState(UIState state)
    {
        currentState = state;
        homeUI.SetActive(currentState);
        inGameUI.SetActive(currentState);
        scoreUI.SetActive(currentState);
        shopUI.SetActive(currentState);
    }
}