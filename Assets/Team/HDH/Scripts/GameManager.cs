using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [field : SerializeField] public int Score { get; private set; }
    [field: SerializeField] public int Money { get; private set; }
    [field: SerializeField] public int Day { get; private set; }
    [field: SerializeField] public float PassedTime { get; private set; }

    [SerializeField] private TextMeshProUGUI timer;

    //날짜 조정
    //시간 흐르게
    //인게임-결산-샵 이동 로직
    //미션 시작 상태 관리

    private void Update()
    {
        PassedTime += Time.deltaTime;
        timer.text = PassedTime.FormatTime();
    }

}
