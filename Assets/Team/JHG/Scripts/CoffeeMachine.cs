using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour
{
    public bool isUse = true;
    public float coffeMachineColltime = 10f;
    public int coffeStress = 10;

    private void Start()
    {
        isUse = true;
    }
    public void DownStress()
    {
        isUse = false;
        Debug.Log("스트레스 감소");
        GameManager.Instance.ChangeStress(-coffeStress);
        StartCoroutine(CoffeeMachineTimer());
    }

    IEnumerator CoffeeMachineTimer()
    {
        while (!isUse)
        {
            coffeMachineColltime -= Time.deltaTime;
            if (coffeMachineColltime < 0)
            {
                isUse = true;
                coffeMachineColltime = 10f;
            }
            yield return null;
        }
    }
}
