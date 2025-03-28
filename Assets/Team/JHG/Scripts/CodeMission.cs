using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CodeMission : MonoBehaviour, IMission
{
    [Header("Panel")]
    [SerializeField] private GameObject errerPanel;
    [SerializeField] private GameObject inspectorPanel;
    [SerializeField] private GameObject selectObjectPanel;

    [Header("Panel Button")]
    [SerializeField] private Button errerButton;
    [SerializeField] private Button unityButton;
    [SerializeField] private Button inspectorButton;
    [SerializeField] private Button applyButton;


    [Header("Mission")]
    [SerializeField] private TextMeshProUGUI missionHintText1; // ErrerPanel 힌트
    [SerializeField] private TextMeshProUGUI missionHintText2; // InspectorPanel 힌트
    [SerializeField] private List<Button> objectListButton = new List<Button>();

    [SerializeField] private List<string> missionTextList = new List<string>();
    [SerializeField] private string answer;

    [Header("Select Text")]
    [SerializeField] private TextMeshProUGUI selectObjectText;
    [SerializeField] private string selectText;

    bool isAnswer = false;
    int score = 0;
    private void Start()
    {
        OnClickStart();

        RandomText();

        inspectorPanel.SetActive(false);
        selectObjectPanel.gameObject.SetActive(false);
    }

    private void OnClickStart() // 모든 버튼 onClick 기능 부여
    {
        errerButton.onClick.AddListener(onClickCancelButton);
        unityButton.onClick.AddListener(OnClickUnityButton);
        inspectorButton.onClick.AddListener(OnClickInspectorButton);
        applyButton.onClick.AddListener(OnClickApplyButton);

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
        Debug.Log(i);
        selectObjectText.text = objectListButton[i].GetComponent<TextMeshProUGUI>().text;
        selectText = selectObjectText.text;
    }
    private void OnClickApplyButton()
    {
        if (answer == selectText)
        {
            Debug.Log("성공");
            isAnswer = true;
        }
        else
        {
            Debug.Log("실패");
            isAnswer=false;
        }
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

        Debug.Log(answer);

        missionHintText1.text = $"NullReferenceException: Object reference not set to an instance of an object\r\nGameManager.{answer} (System.Int32 {answer}) (at Assets/Manager.cs:41)";
        missionHintText2.text = answer;
    }


    
    public void GameEnd()
    {
        Destroy(this.gameObject);
    }

    public int GetScore()
    {
        score = isAnswer ? 0 : 5;
        GetStress();
        return score;
    }

    public float GetStress()
    {
        float stress = 0;
        stress = score > 0 ? 5 : 10;
        return stress;
    }
}
