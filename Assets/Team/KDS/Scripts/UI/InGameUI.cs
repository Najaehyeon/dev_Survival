using UnityEngine;
using UnityEngine.UI;
public class InGameUI : BaseUI
{
    public float stress;
    public RectTransform stressbar;
   
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }

    public void Update()
    {
        MoveStressBar();
    }

    private void MoveStressBar()
    {
        stressbar.anchoredPosition = new Vector2(200 * (stress / 100), 0);
    }
    protected override UIState GetUIState()
    {
        return UIState.InGame;
    }
}
