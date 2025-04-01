using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : BaseUI
{
    bool isEmployShop;
    public RectTransform stressbarInItemShop;
    public RectTransform stressbarInEmplyShop;
    public GameObject notEnoughAlert;

    [Header("Shop")]
    public ItemShop itemShop;
    public EmployShop employShop;

    [Header("Button")]
    public Button buyDog;
    public Button buyCat;
    public Button buyBottle;
    public Button buyCloud;
    public Button closeNotEnoughMoneyAlert;

    private void Start()
    {
        buyDog.onClick.AddListener(ShopManager.Instance.itemShop.BuyDog);
        buyCat.onClick.AddListener(ShopManager.Instance.itemShop.BuyCat);
        buyBottle.onClick.AddListener(ShopManager.Instance.itemShop.BuyGreenBottle);
        buyCloud.onClick.AddListener(ShopManager.Instance.itemShop.BuyCloud);
        closeNotEnoughMoneyAlert.onClick.AddListener(CloseNotEnoughMoneyAlert);
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }

    protected override UIState GetUIState()
    {
        MoveStressBar();
        //Debug.Log("스트레스 바 업그레이드 됨");
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
        itemShop.MonenInit();
        employShop.MoneyInit();
    }

    public void MoveStressBar()
    {
        stressbarInItemShop.anchoredPosition = new Vector2(200 * (UIManager.Instance.inGameUI.stress / 100), 0);
        stressbarInEmplyShop.anchoredPosition = new Vector2(200 * (UIManager.Instance.inGameUI.stress / 100), 0);
    }

    public void CloseNotEnoughMoneyAlert()
    {
        notEnoughAlert.SetActive(false);
    }
}