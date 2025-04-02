using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CodeMission : Mission
{
    [Header("Panel")]
    [SerializeField] private GameObject errerPanel;
    [SerializeField] private GameObject inspectorPanel;
    [SerializeField] private GameObject selectObjectPanel;
    [SerializeField] private GameObject endPanel;

    [Header("Panel Button")]
    [SerializeField] private Button errerButton;
    [SerializeField] private Button unityButton;
    [SerializeField] private Button inspectorButton;
    [SerializeField] private Button applyButton;
    [SerializeField] private Button endButton;


    [Header("Mission")]
    [SerializeField] private TextMeshProUGUI missionHintText1; // ErrerPanel 힌트
    [SerializeField] private TextMeshProUGUI missionHintText2; // InspectorPanel 힌트
    [SerializeField] private List<Button> objectListButton = new List<Button>();

    [SerializeField] private List<string> missionTextList = new List<string>();
    [SerializeField] private string answer;

    [Header("Select Text")]
    [SerializeField] private TextMeshProUGUI selectObjectText;
    [SerializeField] private string selectText;

    [Header("Score Text")]
    [SerializeField] private TextMeshProUGUI completeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    private bool isFail;

    CodeMissionTimer codeMissionTimer;

    private void Start()
    {
        codeMissionTimer = GetComponent<CodeMissionTimer>();

        OnClickStart();

        RandomText();

        inspectorPanel.SetActive(false);
        selectObjectPanel.gameObject.SetActive(false);
        endPanel.SetActive(false);

        codeMissionTimer.startTimer();
    }
    private void Update()
    {
        if (codeMissionTimer.isTimeOver)
        {
            IsFail();
        }
    }

    private void OnClickStart() // 모든 버튼 onClick 기능 부여
    {
        errerButton.onClick.AddListener(onClickCancelButton);
        unityButton.onClick.AddListener(OnClickUnityButton);
        inspectorButton.onClick.AddListener(OnClickInspectorButton);
        applyButton.onClick.AddListener(OnClickApplyButton);
        endButton.onClick.AddListener(OnClickEndButton);

        for (int i = 0; i < objectListButton.Count; i++)
        {
            int index = i;
            objectListButton[i].onClick.AddListener(CloseSelectObjectPanel);
            objectListButton[i].onClick.AddListener(() => ChangeText(index));
        }
    }

    private void onClickCancelButton()
    {
        errerPanel.gameObject.SetActive(false);
    }

    private void OnClickUnityButton()
    {
        inspectorPanel.gameObject.SetActive(true);
    }
    private void OnClickInspectorButton()
    {
        selectObjectPanel.gameObject.SetActive(true);
    }
    private void CloseSelectObjectPanel()
    {
        selectObjectPanel.gameObject.SetActive(false);
    }
    private void ChangeText(int i)
    {
        selectObjectText.text = objectListButton[i].GetComponent<TextMeshProUGUI>().text;
        selectText = selectObjectText.text;
    }

    private void UpdateScore()
    {
        completeText.text = isFail ? "Build failure" : "Build Complete";
        scoreText.text = $"SCORE : {score}";
    }

    private void OnClickApplyButton()
    {
        codeMissionTimer.EndTimer();
        if (answer == selectText && codeMissionTimer.curTime < codeMissionTimer.timer)
        {
            IsAnswer();
        }
        else
        {
            IsFail();
        }
    }
    private void OnClickEndButton()
    {
        GameEnd();
    }

    public void RandomText() // 랜덤으로 정답 고르는 매서드
    {
        missionTextList.Clear();

        selectObjectText.text = "null";

        for (int i = 0; i < objectListButton.Count; i++)
        {
            string text = objectListButton[i].GetComponent<TextMeshProUGUI>().text;
            missionTextList.Add(text);
        }

        int randomNum = Random.Range(0, missionTextList.Count);

        answer = missionTextList[randomNum];

        missionHintText1.text = $"NullReferenceException: Object reference not set to an instance of an object\r\nGameManager.{answer} (System.Int32 {answer}) (at Assets/Manager.cs:41)";
        missionHintText2.text = answer;
    }


    private void IsAnswer()
    {
        score = codeMissionTimer.curTime > 10 ? 3 : 5;
        isFail = false;
        UpdateScore();
        endPanel.SetActive(true);
    }

    private void IsFail()
    {
        score = 0;
        isFail = true;
        UpdateScore();
        endPanel.SetActive(true);
    }

    public override void GameEnd()
    {
        base.GameEnd();
    }
}
