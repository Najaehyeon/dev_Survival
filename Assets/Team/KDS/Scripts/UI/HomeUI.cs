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
                Debug.Log("실행중 클릭");
            }
            else 
            {
                Debug.Log("인게임으로");
                UIManager.Instance.ChangeState(UIState.InGame);
            }
        }
    }

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