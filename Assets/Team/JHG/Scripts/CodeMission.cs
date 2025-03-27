using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CodeMission : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject unityPanel;
    [SerializeField] private GameObject errerPanel;
    [SerializeField] private GameObject inspectorPanel;
    [SerializeField] private GameObject SelectObjectPanel;

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

    private void Start()
    {
        OnClickStart();

        RandomText();

        inspectorPanel.SetActive(false);
        SelectObjectPanel.gameObject.SetActive(false);
    }

    private void OnClickStart()
    {
        errerButton.onClick.AddListener(onClickCancelButton);
        unityButton.onClick.AddListener(OnClickUnityButton);
        inspectorButton.onClick.AddListener(OnClickInspectorButton);

        for (int i = 0; i < objectListButton.Count; i++)
        {
            objectListButton[i].onClick.AddListener(CloseInspectorPanel);
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
        SelectObjectPanel.gameObject.SetActive(true);
    }
    private void CloseInspectorPanel()
    {
        inspectorPanel.gameObject.SetActive(false);
    }

    public void RandomText()
    {
        missionTextList.Clear();
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
