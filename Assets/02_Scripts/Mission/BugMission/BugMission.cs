using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BugMission : Mission
{
    [Header("미션 관련 정보")]
    [SerializeField] float LimitTime = 10f; //Bug 미션 제한 시간
    [SerializeField] GameObject[] bugPrefab; //버그 프리팹
    [SerializeField] List<Bug> Bugs; //현재 활성화 중인 버그
    [SerializeField] Rect fieldRange; //버그가 생성될 영역 범위
    [SerializeField] float aimOffset; //마우스 위치와 버그 위치 간의 허용 오차값
    public float[] thresholdTime = new float[] {5, 8};

    private float passsedTime; //플레이 중 흘러간 시간
    private int killCount; //죽인 버그의 수
    private bool isFail; //실패 여부
    private bool isComplete; //완료 여부
    private float completeTime; //완료시 시간

    private GameObject fieldObj; //버그가 생성되는 부모 오브젝트

    private bool onClick; //마우스 클릭 변수
    private Vector2 mousePos; //마우스 위치 변수
    private Camera mainCam; //메인 카메라
    
    [SerializeField] AudioClip[] bugKillSound = new AudioClip[4];
    
    [Header("UI 요소")]
    [SerializeField] private Button ExitButton;
    [SerializeField] private Button InfoButton;
    [SerializeField] private Button CompleteButton;
    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private Image ProgressBarImage;
    [SerializeField] private TextMeshProUGUI ProgressText;
    [SerializeField] private GameObject CompleteSign;
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private GameObject FailSign;

    private void Start()
    {
        mainCam = Camera.main;
        
        fieldObj = new GameObject("BugField"); //버그가 생성될 영역을 생성
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
            //일정 시간이 지나면 실패로 처리
            isFail = true;
            OnFail();
        }
    }

    private void OnDrawGizmos()
    {
        Color gizmoColor = Color.red;
        gizmoColor.a = 0.5f;
        Gizmos.color = gizmoColor;
        Vector3 center = new Vector3(fieldRange.x + fieldRange.width / 2, fieldRange.y + fieldRange.height / 2);
        Vector3 size = new Vector3(fieldRange.width, fieldRange.height);
        Gizmos.DrawCube(center, size);

    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            onClick = true;
        else if(context.phase == InputActionPhase.Canceled)
            onClick = false;
    }

    public void GetMousePos(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            mousePos = context.ReadValue<Vector2>();
    }

    void KillBug()
    {
        if(onClick)
        {
            Vector3 toWorldPoint = mainCam.ScreenToWorldPoint(mousePos);
            toWorldPoint = new Vector3(toWorldPoint.x, toWorldPoint.y, 0);
            
            foreach (Bug bug in Bugs)
            {
                if ((bug.transform.position - toWorldPoint).magnitude < aimOffset)
                {
                    bug.gameObject.SetActive(false);
                    AudioClip audioClip = bugKillSound[Random.Range(0, bugKillSound.Length)];
                    SoundManager.Instance.PlayClip(audioClip);
                }
            }
            
            KillCheck();
            UpdateProgressBar();
            
            isComplete = killCount == Bugs.Count;
            
            onClick = false;
        }
    }

    void KillCheck()
    {
        killCount = 0;
        
        foreach (Bug bug in Bugs)
        {
            killCount += bug.gameObject.activeSelf ?  0 : 1;
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
        ScoreText.text = "+" + GetScroe().ToString();
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
            base.GameEnd();
        }
    }

    public override int GetScroe()
    {
        if (isFail) score = 1;
        else
        {
            if (completeTime < thresholdTime[0])
                score = 5;
            else if (completeTime < thresholdTime[1])
                score = 3;
            else
                score = 1;
        }
        return score;
    }

}
