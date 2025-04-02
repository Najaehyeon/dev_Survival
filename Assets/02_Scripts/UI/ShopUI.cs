using UnityEngine;
using UnityEngine.UI;

public class ShopUI : BaseUI
{
    bool isEmployShop;
    public RectTransform stressbarInItemShop;
    public RectTransform stressbarInEmplyShop;
    public GameObject notEnoughMoneyAlert;
    public GameObject notEnoughStressAlert;

    [Header("Shop")]
    public ItemShop itemShop;
    public EmployShop employShop;

    [Header("Button")]
    public Button buyDog;
    public Button buyCat;
    public Button buyBottle;
    public Button buyCloud;
    public Button closeNotEnoughMoneyAlert;
    public Button closeNotEnoughStressAlert;

    private void Start()
    {
        buyDog.onClick.AddListener(itemShop.BuyDog);
        buyCat.onClick.AddListener(itemShop.BuyCat);
        buyBottle.onClick.AddListener(itemShop.BuyGreenBottle);
        buyCloud.onClick.AddListener(itemShop.BuyCloud);
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }

    protected override UIState GetUIState()
    {
        MoveStressBar();
        return UIState.Shop;
    }

    public void OnExitButton()
    {
        UIManager.Instance.ChangeState(UIState.InGame);
        GameManager.Instance.stateMachine.ChangeState(GameManager.Instance.stateMachine.inGameState);
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
        MoneyInit();
    }

    public void MoveStressBar()
    {
        stressbarInItemShop.anchoredPosition = new Vector2(200 * (UIManager.Instance.inGameUI.stress / 100), 0);
        stressbarInEmplyShop.anchoredPosition = new Vector2(200 * (UIManager.Instance.inGameUI.stress / 100), 0);
    }

    public void CloseNotEnoughAlert()
    {
        notEnoughMoneyAlert.SetActive(false);
        notEnoughStressAlert.SetActive(false);
    }

    public void MoneyInit()
    {
        itemShop.moneyInItemShop.text = GameManager.Instance.Money.ToString() + "만원";
        employShop.moneyInEmployShop.text = GameManager.Instance.Money.ToString() + "만원";
    }
}
