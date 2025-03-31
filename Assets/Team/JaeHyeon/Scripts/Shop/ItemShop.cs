using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    private bool buyCat;
    private bool buyDog;
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
    public void BuyDog()
    {
        if (!HaveMoney(animalsPrice)) return;
    }
    public void BuyCat()
    {
        if (!HaveMoney(animalsPrice)) return;
    }

    public void BuyGreenBottle()
    {
        if (!HaveMoney(greenPrice)||GameManager.Instance.Stress<50) return;
        GameManager.Instance.ChangeStress(-50);
        GameManager.Instance.ChangeMoney(-greenPrice);
    }
    public void BuyCloud()
    {
        if (!HaveMoney(cloudPrice) || GameManager.Instance.Stress < 10) return;
        GameManager.Instance.ChangeStress(-10);
        GameManager.Instance.ChangeMoney(-cloudPrice);
    }

    private bool HaveMoney(int price)
    {
        if (GameManager.Instance.Money >= price) return true;
        else return false;
    }

}
