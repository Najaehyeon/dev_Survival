using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
public class ScoreUI : BaseUI
{
    [Header("UI")]
    public GameObject ScorePannel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI stressText;
    public Button Shop;

    public TextMeshProUGUI buttonText;

    public AudioClip clip;

    private RectTransform rect;

    private int score;
    private int money;
    private int endmoney;
    private int stress;
    private Coroutine coroutine;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        rect = ScorePannel.GetComponent<RectTransform>();

    }
    public void OnEnable()
    {
        RectTransform rect = ScorePannel.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(-1500, 0);
        score= GameManager.Instance.Score;
        money = GameManager.Instance.Money;
        endmoney = money + (score*10);
        scoreText.text = score.ToString();
        moneyText.text = money.ToString();
        stressText.text = "";
        Shop.interactable = false;
        rect.DOAnchorPos(new Vector2(0, 0), 1f).SetEase(Ease.OutCubic)
            .OnKill(() => coroutine = StartCoroutine(StringChange()));

    }

    public void OnShopButton()
    {
        rect.DOAnchorPos(new Vector2(1500, 0), 1f).SetEase(Ease.InCubic)
        .OnKill(() => {
            UIManager.Instance.ChangeState(UIState.Shop);
            GameManager.Instance.stateMachine.ChangeState(GameManager.Instance.stateMachine.shopState);
        });
    }
    /// <summary>
    /// 점수가 돈으로 바뀌는 연출
    /// </summary>
    /// <returns></returns>
    public IEnumerator StringChange()
    {
        SoundManager.instance.PlayClip(clip);
        while (score != 0)
        {
            score--;
            scoreText.text = score.ToString();
            money+=10;
            moneyText.text = money.ToString() + "만원";
            yield return new WaitForSeconds(0.02f);
        }
        scoreText.text = "0";
        moneyText.text = endmoney.ToString() + "만원";
        StartCoroutine(TypeStress());
    }

    public IEnumerator TypeStress()
    {
        string stress = $"{GameManager.Instance.Stress} -> {GameManager.Instance.Stress / 2}";

        foreach (char letter in stress)
        {
            stressText.text += letter;  
            yield return new WaitForSeconds(0.1f); 
        }

        Shop.interactable = true;
    }
    protected override UIState GetUIState()
    {
        return UIState.Score;
    }

    public void ChangeText()
    {
        buttonText.text = "화성...?";
    }
}