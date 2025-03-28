using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeMissionTimer : MonoBehaviour
{
    [SerializeField] private Image timerUI;

    public float timer;
    public float curTime;
    public bool isTimeOver = false;

    public void startTimer()
    {
        curTime = 0;
        StartCoroutine(StartTimerCoroutine());
    }

    public void EndTimer()
    {
        StopCoroutine(StartTimerCoroutine());
    }

    IEnumerator StartTimerCoroutine()
    {
        while (curTime < timer)
        {
            curTime += Time.deltaTime;
            timerUI.fillAmount = 1 - (curTime / timer);
            yield return null;
        }
        isTimeOver = true;
        EndTimer();
    }
}
