using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MissionController : MonoBehaviour
{
    private float timer = 0f;
    private float minInterval = 3f; // 최소 3초
    private float maxInterval = 5f; // 최대 5초
    private float currentInterval;

    private bool nowMissionphase;

    public Mission[] missons;
    public MissionTimer[] missonTimers;
    void Start()
    {
        missons = new Mission[2];
        missonTimers = new MissionTimer[5];
        currentInterval = UnityEngine.Random.Range(minInterval, maxInterval);
    }

    private void Update()
    {
        if (!nowMissionphase)
        {

            timer += Time.deltaTime;

            if (timer >= currentInterval)
            {
                timer = 0f;
                MissionSelector();
                currentInterval = UnityEngine.Random.Range(minInterval, maxInterval);                
                nowMissionphase = true;
                Debug.Log(nowMissionphase);
            }
        }

    }

    public void MissionSelector()
    {
        System.Random rng = new System.Random();
        //missonTimers = missonTimers.OrderBy(x => rng.Next()).ToArray();

        ///테스트단계에서 하나밖에없어서 실행하면 Rarry오류남
        //int missionCount = UnityEngine.Random.Range(1, 3);
        //for(int i=0;i<missionCount;i++)
        //{
        //    missonTimers[i].gameObject.SetActive(true);
        //    missonTimers[i].Selected();
        //}

        missonTimers[0].gameObject.SetActive(true);
        missonTimers[0].Selected();
        missons[0] = missonTimers[0].mission;
    }

    public void IsAllGameEnd()
    {
        bool allGameEnd = true;
        foreach (Mission mission in missons)
        {
            if (!mission.isGameEnd)
            {
                allGameEnd = false;
                break;
            }
        }
        if (allGameEnd)
        {
            Debug.Log("모든게임 끝");
        }
    }
}
