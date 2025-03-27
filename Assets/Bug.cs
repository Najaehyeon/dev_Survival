using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bug : MonoBehaviour
{
    [Header("미션 관련 정보")]
    private float passsedTime;
    [SerializeField] float LimitTime = 10f;
    [SerializeField] GameObject[] Bugs;

    [Header("UI 요소")]
    [SerializeField] private Button ExitButton;
    [SerializeField] private Button InfoButton;
    [SerializeField] private Button CompleteButton;
    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private Image ProgressBarImage;
    [SerializeField] private TextMeshProUGUI ProgressText;

    private void Update()
    {
        if (passsedTime >= LimitTime) return;

        passsedTime += Time.deltaTime;
        TimerText.text = passsedTime.FormatTime2();
    }

}
