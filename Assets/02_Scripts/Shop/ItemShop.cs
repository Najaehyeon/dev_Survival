using TMPro;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    public TextMeshProUGUI moneyInItemShop;

    public bool hasDog = false;
    public bool hasCat = false;
    private int animalsPrice;
    private int greenPrice;
    private int cloudPrice;

    private ShopUI shopUI;

    private void Awake()
    {
        animalsPrice = 60;
        greenPrice = 20;
        cloudPrice = 10;
    }

    private void Start()
    {
        shopUI = UIManager.Instance.shopUI;
        shopUI.MoneyInit();
    }

    public void BuyDog()
    {
        if (hasDog) return;
        if (!HaveMoney(animalsPrice))
        {
            shopUI.notEnoughMoneyAlert.SetActive(true);
            shopUI.closeNotEnoughMoneyAlert.onClick.AddListener(shopUI.CloseNotEnoughAlert);
            return;
        }
        GameManager.Instance.ChangeMoney(-animalsPrice);
        shopUI.MoneyInit();
        hasDog = true;
        shopUI.buyDog.gameObject.SetActive(false);
        NPCManager.Instance.SpawnDog();
    }

    public void BuyCat()
    {
        if (hasCat) return;
        if (!HaveMoney(animalsPrice))
        {
            shopUI.notEnoughMoneyAlert.SetActive(true);
            shopUI.closeNotEnoughMoneyAlert.onClick.AddListener(shopUI.CloseNotEnoughAlert);
            return;
        }
        GameManager.Instance.ChangeMoney(-animalsPrice);
        shopUI.MoneyInit();
        hasCat = true;
        shopUI.buyCat.gameObject.SetActive(false);
        NPCManager.Instance.SpawnCat();
    }

    public void BuyGreenBottle()
    {
        if (GameManager.Instance.Stress < 50)
        {
            shopUI.notEnoughStressAlert.SetActive(true);
            shopUI.closeNotEnoughStressAlert.onClick.AddListener(shopUI.CloseNotEnoughAlert);
            return;
        }
        if (!HaveMoney(greenPrice))
        {
            shopUI.notEnoughMoneyAlert.SetActive(true);
            shopUI.closeNotEnoughMoneyAlert.onClick.AddListener(shopUI.CloseNotEnoughAlert);
            return;
        }
        GameManager.Instance.ChangeStress(-50);
        GameManager.Instance.ChangeMoney(-greenPrice);
        shopUI.MoneyInit();
        shopUI.MoveStressBar();
    }

    public void BuyCloud()
    {
        if (GameManager.Instance.Stress < 10)
        {
            shopUI.notEnoughStressAlert.SetActive(true);
            shopUI.closeNotEnoughStressAlert.onClick.AddListener(shopUI.CloseNotEnoughAlert);
            return;
        }
        if (!HaveMoney(cloudPrice))
        {
            shopUI.notEnoughMoneyAlert.SetActive(true);
            shopUI.closeNotEnoughMoneyAlert.onClick.AddListener(shopUI.CloseNotEnoughAlert);
            return;
        }
        GameManager.Instance.ChangeStress(-10);
        GameManager.Instance.ChangeMoney(-cloudPrice);
        shopUI.MoneyInit();
        shopUI.MoveStressBar();
    }

    private bool HaveMoney(int price)
    {
        if (GameManager.Instance.Money >= price) return true;
        else return false;
    }
}
