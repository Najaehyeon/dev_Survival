using UnityEngine;
using UnityEngine.UI;

public class ShopUI : BaseUI
{
    bool isEmployShop;
    [Header("Shop")]
    public ItemShop itemShop;
    public EmployShop employShop;

    [Header("Button")]
    public Button buyDog;
    public Button buyCat;

    private void Start()
    {
        buyDog.onClick.AddListener(ShopManager.Instance.itemShop.BuyDog);
        buyCat.onClick.AddListener(ShopManager.Instance.itemShop.BuyCat);
    }

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
        GameManager.Instance.stateMachine.ChangeState(GameManager.Instance.stateMachine.inGameState);
        // 플레이어 시작 위치 초기화
        // 미션 스테이트 초기화
        // 
    }

    public void OnChangeShopstate()
    {
        isEmployShop = !isEmployShop;
        if(isEmployShop)
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