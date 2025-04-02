using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor.Experimental.GraphView;

public enum MissionState
{
    Phase,
    Ready,
    Mission
}
public enum MissionSelect
{
    Bug,
    Call,
    Code,
    Server
}
public class MissionController : MonoBehaviour
{
    private float timer = 0f;
    private float minInterval = 3f; // 최소 3초
    private float maxInterval = 5f; // 최대 5초
    private float currentInterval;

    //게임 정지시 사용할 이전 스테이트
    //private MissionState preState;
    private MissionState currentState;
    public MissionSelect[] missions;
    public MissionTimer[] call;
    public MissionTimer[] code;
    public MissionTimer[] bug;
    public MissionTimer server;

    public Canvas canvas;
    private int missionCount;
    void Start()
    {
        ChangeState(MissionState.Phase);
        currentInterval = UnityEngine.Random.Range(minInterval, maxInterval);
        missions = new MissionSelect[]{ MissionSelect.Bug, MissionSelect.Call, MissionSelect.Code, MissionSelect.Server };
    }

    private void Update()
    {
        if (currentState == MissionState.Ready)
        {

            timer += Time.deltaTime;

            if (timer >= currentInterval)
            {
                timer = 0f;
                MissionSelector();
                currentInterval = UnityEngine.Random.Range(minInterval, maxInterval);
                ChangeState(MissionState.Mission);
            }
        }

    }

    /// <summary>
    /// 랜덤한 위치에 미션타이머 Active
    /// </summary>
    public void MissionSelector()
    {
        System.Random rng = new System.Random();
        missions = missions.OrderBy(x => rng.Next()).ToArray();
        missionCount = UnityEngine.Random.Range(1, 3);
        for (int i = 0; i < missionCount; i++)
        {
            if(missions[i]!= MissionSelect.Server)
            {
                int randomindex = UnityEngine.Random.Range(0, 5);
                switch(missions[i]){
                    case MissionSelect.Bug:
                        bug[randomindex].gameObject.SetActive(true);
                        bug[randomindex].Selected();
                        break;
                    case MissionSelect.Call:
                        call[randomindex].gameObject.SetActive(true);
                        call[randomindex].Selected();
                        break;
                    case MissionSelect.Code:
                        code[randomindex].gameObject.SetActive(true);
                        code[randomindex].Selected();
                        break;
                }
            }
            else
            {
                server.gameObject.SetActive(true);
                server.Selected();
            }
        }
    }
   
    /// <summary>
    /// 미션이 끝날때마다 남아있는 미션이있는지 탐색
    /// </summary>
    public void IsAllGameEnd()
    {
        if (MissionManager.Instance.SelectedMissions.Count == 0) ChangeState(MissionState.Ready);
    }

    /// <summary>
    /// 게임 상태가 인게임->결산 으로 전환댈때 켜져있는 게임 전부 삭제
    /// </summary>
    public void IsDayEnd()
    {
        missionCount = 0;
        currentState = MissionState.Phase;
        foreach(MissionTimer missionTimer in MissionManager.Instance.SelectedMissions)
        {
            missionTimer.IsDayEnd();
        }
        MissionManager.Instance.SelectedMissions.Clear();
    }

    public void ChangeState(MissionState state)
    {
        switch (state)
        {
            case MissionState.Phase:
                currentState = MissionState.Phase;
                break;
            case MissionState.Ready: 
                currentState = MissionState.Ready; 
                break;
            case MissionState.Mission:
                currentState = MissionState.Mission;
                break;
        }
    }
}
