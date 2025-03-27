using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BugMission : MonoBehaviour
{
    [Header("미션 관련 정보")]
    private float passsedTime;
    [SerializeField] float LimitTime = 10f;
    [SerializeField] GameObject[] bugPrefab;
    [SerializeField] List<Bug> Bugs;
    [SerializeField] Rect fieldRange;

    [Header("UI 요소")]
    [SerializeField] private Button ExitButton;
    [SerializeField] private Button InfoButton;
    [SerializeField] private Button CompleteButton;
    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private Image ProgressBarImage;
    [SerializeField] private TextMeshProUGUI ProgressText;

    private void Start()
    {
        GameObject fieldObj = new GameObject("BugField");
        fieldObj.transform.position = new Vector3(fieldRange.x, fieldRange.y, 0);

        for(int i = 0; i < 5; i++)
        {
            Bugs.Add(Instantiate(bugPrefab[Random.Range(0, bugPrefab.Length)], fieldObj.transform).GetComponent<Bug>());
            Bugs[i].FieldRange = fieldRange;
        }
    }

    private void Update()
    {
        if (passsedTime >= LimitTime) return;

        passsedTime += Time.deltaTime;
        TimerText.text = passsedTime.FormatTime2();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red * 0.1f;
        Vector3 center = new Vector3(fieldRange.x + fieldRange.width / 2, fieldRange.y + fieldRange.height / 2);
        Vector3 size = new Vector3(fieldRange.width, fieldRange.height);
        Gizmos.DrawCube(center, size);

    }

}
