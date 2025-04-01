using TMPro;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    public TextMeshProUGUI moneyInItemShop;

    private bool hasDog = false;
    private bool hasCat = false;
    private int animalsPrice;
    private int greenPrice;
    private int cloudPrice;

    private void Awake()
    {
        ShopManager.Instance.itemShop = this;
        animalsPrice = 60;
        greenPrice = 20;
        cloudPrice = 10;
    }

    private void Start()
    {
        MonenInit();
    }

    public void BuyDog()
    {
        if (hasCat) return;
        if (!HaveMoney(animalsPrice))
        {
            UIManager.Instance.shopUI.notEnoughAlert.SetActive(true);
            UIManager.Instance.shopUI.closeNotEnoughMoneyAlert.onClick.AddListener(UIManager.Instance.shopUI.CloseNotEnoughMoneyAlert);
            return;
        }
        GameManager.Instance.ChangeMoney(-animalsPrice);
        MonenInit();
        hasDog = true;
        UIManager.Instance.shopUI.buyDog.gameObject.SetActive(false);
    }

    public void BuyCat()
    {
        if (hasCat) return;
        if (!HaveMoney(animalsPrice))
        {
            UIManager.Instance.shopUI.notEnoughAlert.SetActive(true);
            UIManager.Instance.shopUI.closeNotEnoughMoneyAlert.onClick.AddListener(UIManager.Instance.shopUI.CloseNotEnoughMoneyAlert);
            return;
        }
        GameManager.Instance.ChangeMoney(-animalsPrice);
        MonenInit();
        hasCat = true;
        UIManager.Instance.shopUI.buyCat.gameObject.SetActive(false);
    }

    public void BuyGreenBottle()
    {
        if (!HaveMoney(greenPrice) || GameManager.Instance.Stress < 50)
        {
            UIManager.Instance.shopUI.notEnoughAlert.SetActive(true);
            UIManager.Instance.shopUI.closeNotEnoughMoneyAlert.onClick.AddListener(UIManager.Instance.shopUI.CloseNotEnoughMoneyAlert);
            return;
        }
        GameManager.Instance.ChangeStress(-50);
        GameManager.Instance.ChangeMoney(-greenPrice);
        MonenInit();
        UIManager.Instance.shopUI.MoveStressBar();
    }

    public void BuyCloud()
    {
        if (!HaveMoney(cloudPrice) || GameManager.Instance.Stress < 10)
        {
            UIManager.Instance.shopUI.notEnoughAlert.SetActive(true);
            UIManager.Instance.shopUI.closeNotEnoughMoneyAlert.onClick.AddListener(UIManager.Instance.shopUI.CloseNotEnoughMoneyAlert);
            return;
        }
        GameManager.Instance.ChangeStress(-10);
        GameManager.Instance.ChangeMoney(-cloudPrice);
        MonenInit();
        UIManager.Instance.shopUI.MoveStressBar();
    }

    private bool HaveMoney(int price)
    {
        if (GameManager.Instance.Money >= price) return true;
        else return false;
    }

    public void MonenInit()
    {
        moneyInItemShop.text = GameManager.Instance.Money.ToString() + "\\";
    }
}
