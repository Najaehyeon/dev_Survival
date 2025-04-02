using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Status
{
    Day,
    Time,
    Score,
    Stress,
    Money
}
public class InGameUI : BaseUI
{
    [Header("Status")]
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public RectTransform stressbar;
    public TextMeshProUGUI moneyText;

    [Header("Pause")]
    public GameObject pauseUI;
    public Button pauseButton;
    public Button resumeButton;
    public Button resetButton;

    [Header("Value")]
    public int day;
    public float time;
    public int score;
    public float stress;
    public int money;
    public void Start()
    {
        pauseUI.SetActive(false);
        pauseButton.onClick.AddListener(OnPauseBotton);
        resumeButton.onClick.AddListener(OnResumeBotton);
        resetButton.onClick.AddListener(OnResetBotton);
    }
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }
    private void MoveStressBar()
    {
        stressbar.anchoredPosition = new Vector2(200 * (stress / 100), 0);
    }
    protected override UIState GetUIState()
    {
        return UIState.InGame;
    }

    public void ChangeStatus(Status status,int value)
    {
        switch (status)
        {
            case Status.Day:
                day = value;
                dayText.text=value.ToString()+" 일";
                break;

            case Status.Score:
                score = value;
                scoreText.text=value.ToString();
                break;

            case Status.Money:
                money = value;
                moneyText.text=value.ToString() + "만원";
                break;
        }
    }
    public void ChangeStatus(Status status, float value)
    {
        switch (status)
        {
            case Status.Stress:
                stress = value;
                MoveStressBar();
                break;

            case Status.Time:
                time = value;
                timeText.text=value.FormatTime();
                break;
        }
    }

    public void OnPauseBotton()
    {
        Time.timeScale = 0f;
        pauseUI.SetActive(true);
        GameManager.Instance.isMissionInProgress=true;
    }

    public void OnResumeBotton()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
        GameManager.Instance.isMissionInProgress = false;
    }

    public void OnResetBotton()
    {
        Time.timeScale = 1f;
        DataManager.Instance.DeleteGameManager();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
