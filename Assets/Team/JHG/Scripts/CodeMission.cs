using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CodeMission : MonoBehaviour
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
    [SerializeField] private List<TextMeshProUGUI> missionHintText = new List<TextMeshProUGUI>();
    [SerializeField] private List<Button> objectListButton = new List<Button>();

    [SerializeField] private List<string> missionTextList = new List<string>();
    [SerializeField] private string answer;

    [Header("Select Text")]
    [SerializeField] private TextMeshProUGUI selectObjectText;
    [SerializeField] private string selectText;


    private void Start()
    {
        OnClickStart();

        RandomText();

        inspectorPanel.SetActive(false);
        selectObjectPanel.gameObject.SetActive(false);
    }

    private void OnClickStart()
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
        }
        else
        {
            Debug.Log("실패");
        }
    }

    public void RandomText()
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

        for (int i = 0; i < missionHintText.Count; i++)
        {
            missionHintText[i].text = answer;
        }
    }
}
