using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CoffeeMachine : MonoBehaviour
{
    public bool isUse = true;
    public float coffeMachineCooltime = 10f;
    public int coffeStress = 10;

    [SerializeField] private Image timerImage;

    public AudioClip audioClip;
    private void Start()
    {
        isUse = true;
    }
    public void DownStress()
    {
        isUse = false;
        SoundManager.Instance.PlayClip(audioClip);
        GameManager.Instance.ChangeStress(-coffeStress);
        StartCoroutine(CoffeeMachineTimer());
    }

    IEnumerator CoffeeMachineTimer()
    {
        while (!isUse)
        {
            coffeMachineCooltime -= Time.deltaTime;
            timerImage.fillAmount = 1 - (coffeMachineCooltime / 10f);
            if (coffeMachineCooltime < 0)
            {
                isUse = true;
                coffeMachineCooltime = 10f;
                timerImage.fillAmount = 0;
            }
            yield return null;
        }
    }
}
