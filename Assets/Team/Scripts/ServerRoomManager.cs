using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ServerRoomManager : MonoBehaviour
{
    [SerializeField] private ServerRoom serverRoom;
    [SerializeField] private float missionTime = 10f;

    [SerializeField] private RectTransform[] destinations;

    private float timer;
    private bool missionActive = false;

    private void OnEnable()
    {
        StartMission();
    }

    void Update()
    {
        if (missionActive)
        {
            timer -= Time.deltaTime;
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
        // 상황별로 미션 끝났을 때 보여줄 UI 필요
    }

    private void AssignRandomPositions()
    {
        List<float> yPositions = new List<float> { -300f, 0f, 300f };
        yPositions.Shuffle(); // 리스트 섞기

        for (int i = 0; i < destinations.Length; i++)
        {
            Vector2 newPos = destinations[i].anchoredPosition;
            newPos.y = yPositions[i]; // y값만 설정
            destinations[i].anchoredPosition = newPos;
        }
    }
}

// 리스트 섞는 확장 메서드 추가
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
