using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployShop : MonoBehaviour
{
    private void Awake()
    {
        ShopManager.Instance.employShop = this;
    }

    void Employ()
    {
        // 직원 구매만 (직원 생성은 따로)
    }
}
