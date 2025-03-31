using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class ScoreUI : BaseUI
{
    public GameObject ScorePannel;
    private RectTransform rect;
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        rect = ScorePannel.GetComponent<RectTransform>();

    }
    public void OnEnable()
    {
        RectTransform rect = ScorePannel.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(-1500, 0);
        rect.DOAnchorPos(new Vector2(0, 0), 1f).SetEase(Ease.OutCubic);

    }

    public void OnShopButton()
    {
        rect.DOAnchorPos(new Vector2(1500, 0), 1f).SetEase(Ease.InCubic)
        .OnKill(() => UIManager.Instance.ChangeState(UIState.Shop));
    }
    protected override UIState GetUIState()
    {
        return UIState.Score;
    }
}