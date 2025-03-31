using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployShop : MonoBehaviour
{
    private void Awake()
    {
        ShopManager.Instance.employShop = this;
    }
}
