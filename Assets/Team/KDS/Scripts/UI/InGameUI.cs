using TMPro;
using UnityEngine;
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
    [Header("UI")]
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public RectTransform stressbar;
    public TextMeshProUGUI moneyText;   

    [Header("Value")]
    public int day;
    public float time;
    public int score;
    public float stress;
    public int money;



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
                moneyText.text=value.ToString();
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
}
