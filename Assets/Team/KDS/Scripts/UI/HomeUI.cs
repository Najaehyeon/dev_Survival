using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class HomeUI : BaseUI
{
    RectTransform rectTransform;
    private Tweener bounceTweener;

 
    public float duration = 0.5f;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 800);
    }
    public void Start()
    {
        Production();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (bounceTweener != null && bounceTweener.IsPlaying())
            {
                bounceTweener.Kill(); 
                rectTransform.anchoredPosition = Vector2.zero;
            }
            else 
            {
                UIManager.Instance.ChangeState(UIState.InGame);
                GameManager.Instance.stateMachine.ChangeState(GameManager.Instance.stateMachine.inGameState);
            }
        }
    }

    /// <summary>
    /// 타이틀 로고가 튕기는 효과
    /// </summary>
    private void Production()
    {
        bounceTweener=rectTransform.DOAnchorPos(new Vector2(0, 0), duration)
            .SetEase(Ease.OutBounce)
            .OnKill(() => rectTransform.anchoredPosition = Vector2.zero);

    }
    protected override UIState GetUIState()
    {
        return UIState.Home;
    }
}