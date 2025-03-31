using UnityEngine.UI;
public class ShopUI : BaseUI
{
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }
    protected override UIState GetUIState()
    {
        return UIState.Shop;
    }
}