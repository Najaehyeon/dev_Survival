using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    private void Awake()
    {
        ShopManager.Instance.itemShop = this;
    }

    public void BuyItem()
    {

    }
}
