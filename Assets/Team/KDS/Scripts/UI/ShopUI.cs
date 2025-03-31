using UnityEngine;
using UnityEngine.UI;

public class ShopUI : BaseUI
{
    bool state;
    [Header("Shop")]
    public ItemShop itemShop;
    public EmployShop employShop;

    [Header("Button")]
    public Button buyDog;
    public Button buyCat;
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }
    protected override UIState GetUIState()
    {
        return UIState.Shop;
    }

    public void OnExitButton()
    {
        //게임매니저에서 초기화
        //초기화 항목
        //게임시간 , 스테이트들,
        UIManager.Instance.ChangeState(UIState.InGame);
    }
    public void OnChangeShopstate()
    {
        state = !state;
        if(state)
        {
            employShop.gameObject.SetActive(true);
            itemShop.gameObject.SetActive(false);
        }
        else
        {
            employShop.gameObject.SetActive(false);
            itemShop.gameObject.SetActive(true);
        }
    }
}