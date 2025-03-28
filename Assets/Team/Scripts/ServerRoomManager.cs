using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ServerRoomManager : MonoBehaviour
{
    [SerializeField] private ServerRoom serverRoom;
    [SerializeField] private float missionTime = 10f;

    [SerializeField] private RectTransform[] destinations;

    [SerializeField] private GameObject missionComplete;
    [SerializeField] private GameObject timeOver;
    [SerializeField] private GameObject missionFail;

    [SerializeField] private TextMeshProUGUI timeOverText;
    [SerializeField] private TextMeshProUGUI timerText;

    private float timer;
    private bool missionActive = false;

    private void Start()
    {
        StartMission();
    }

    void Update()
    {
        if (missionActive)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("F0");
            if (timer <= 0 || serverRoom.completedConnections == 3)
            {
                EndMission();
            }
        }
    }

    public void StartMission()
    {
        timer = missionTime;
        missionActive = true;

        AssignRandomPositions();
    }

    private void EndMission()
    {
        missionActive = false;
        int score = serverRoom.GiveScore();
        if (timer <= 0 && serverRoom.completedConnections > 0)
        {
            timeOver.SetActive(true);
            timeOverText.text = "Ÿ�� ����!\n" + serverRoom.completedConnections + " / 3 ����\n" + serverRoom.GiveScore() + "�� ȹ��!";
        }
        else if (timer <= 0 && serverRoom.completedConnections == 0)
        {
            missionFail.SetActive(true);
        }
        else if (serverRoom.completedConnections == 3)
        {
            missionComplete.SetActive(true);
        }
        // ��Ȳ���� �̼� ������ �� ������ UI �ʿ�

    }

    private void AssignRandomPositions()
    {
        List<float> yPositions = new List<float> { -300f, 0f, 300f };
        yPositions.Shuffle(); // ����Ʈ ����

        for (int i = 0; i < destinations.Length; i++)
        {
            Vector2 newPos = destinations[i].anchoredPosition;
            newPos.y = yPositions[i]; // y���� ����
            destinations[i].anchoredPosition = newPos;
        }
    }
}

// ����Ʈ ���� Ȯ�� �޼��� �߰�
public static class ListExtensions
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }
}
