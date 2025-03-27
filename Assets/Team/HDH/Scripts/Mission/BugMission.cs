using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BugMission : MonoBehaviour
{
    [Header("미션 관련 정보")]
    [SerializeField] float LimitTime = 10f;
    [SerializeField] GameObject[] bugPrefab;
    [SerializeField] List<Bug> Bugs;
    [SerializeField] Rect fieldRange;
    [SerializeField] float aimOffset;

    private float passsedTime;
    private int killCount;
    private bool isFail;
    private bool isComplete;

    private float completeTime;

    private GameObject fieldObj;

    [Header("UI 요소")]
    [SerializeField] private Button ExitButton;
    [SerializeField] private Button InfoButton;
    [SerializeField] private Button CompleteButton;
    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private Image ProgressBarImage;
    [SerializeField] private TextMeshProUGUI ProgressText;
    [SerializeField] private GameObject CompleteSign;
    [SerializeField] private GameObject FailSign;

    private void Start()
    {
        fieldObj = new GameObject("BugField");
        fieldObj.transform.position = new Vector3(fieldRange.x, fieldRange.y, 0);

        for (int i = 0; i < 5; i++)
        {
            Bugs.Add(Instantiate(bugPrefab[Random.Range(0, bugPrefab.Length)], fieldObj.transform).GetComponent<Bug>());
            Bugs[i].FieldRange = fieldRange;
        }

        CompleteButton.onClick.AddListener(OnComplete);
        ExitButton.onClick.AddListener(OnExit);

        UpdateProgressBar();
    }

    private void Update()
    {
        if (passsedTime < LimitTime && !isComplete)
        {
            passsedTime += Time.deltaTime;
            TimerText.text = passsedTime.FormatTime2();

            KillBug();
        }
        else if(passsedTime > LimitTime)
        {
            isFail = true;
            OnFail();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red * 0.1f;
        Vector3 center = new Vector3(fieldRange.x + fieldRange.width / 2, fieldRange.y + fieldRange.height / 2);
        Vector3 size = new Vector3(fieldRange.width, fieldRange.height);
        Gizmos.DrawCube(center, size);

    }

    void KillBug()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetMouseButtonDown(0))
        {
            foreach (Bug bug in Bugs)
            {
                if ((bug.transform.position - (Vector3)mousePos).magnitude < aimOffset)
                {
                    killCount++;
                    bug.gameObject.SetActive(false);

                    isComplete = killCount == Bugs.Count;

                    UpdateProgressBar();
                }
            }
        }
    }

    void OnFail()
    {
        FailSign.SetActive(true);

        Bugs.Clear();
        Destroy(fieldObj);
    }

    void OnComplete()
    {
        if (!isComplete) return;
        completeTime = passsedTime;
        CompleteSign.SetActive(true);
        Bugs.Clear();
        Destroy(fieldObj);
    }

    void UpdateProgressBar()
    {
        ProgressBarImage.fillAmount = (float)killCount / Bugs.Count;
        ProgressText.text = (((float)killCount / Bugs.Count) * 100).ToString() + "%";
    }

    void OnExit()
    {
        if (isFail || isComplete)
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

}
