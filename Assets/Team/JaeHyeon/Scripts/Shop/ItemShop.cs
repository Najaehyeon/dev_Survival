using TMPro;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    public GameObject DogPayButton;
    public GameObject CatPayButton;

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
        if (!HaveMoney(animalsPrice) || hasDog)
        {
            Debug.Log("돈 없음");
            return;
        }
        GameManager.Instance.ChangeMoney(-animalsPrice);
        MonenInit();
        hasDog = true;
        DogPayButton.SetActive(false);
    }

    public void BuyCat()
    {
        if (!HaveMoney(animalsPrice) || hasCat)
        {
            Debug.Log("돈 없음");
            return;
        }
        GameManager.Instance.ChangeMoney(-animalsPrice);
        MonenInit();
        hasCat = true;
        CatPayButton.SetActive(false);
    }

    public void BuyGreenBottle()
    {
        if (!HaveMoney(greenPrice)||GameManager.Instance.Stress<50) return;
        GameManager.Instance.ChangeStress(-50);
        GameManager.Instance.ChangeMoney(-greenPrice);
        MonenInit();
        UIManager.Instance.shopUI.MoveStressBar();
    }

    public void BuyCloud()
    {
        if (!HaveMoney(cloudPrice) || GameManager.Instance.Stress < 10) return;
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
