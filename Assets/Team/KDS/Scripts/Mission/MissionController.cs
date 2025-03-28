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
    private bool nowMissionState;

    public MissionTimer[] missonTimers;

    private int missionCount;
    private int claerCount;
    void Start()
    {
        currentInterval = UnityEngine.Random.Range(minInterval, maxInterval);
    }

    private void Update()
    {
        if (!nowMissionState)
        {

            timer += Time.deltaTime;

            if (timer >= currentInterval)
            {
                timer = 0f;
                MissionSelector();
                currentInterval = UnityEngine.Random.Range(minInterval, maxInterval);
                nowMissionState = true;
                Debug.Log(nowMissionState);
            }
        }

    }

    public void MissionSelector()
    {
        
        //System.Random rng = new System.Random();
        //missonTimers = missonTimers.OrderBy(x => rng.Next()).ToArray();

        ///테스트단계에서 하나밖에없어서 실행하면 Rarry오류남
        //missionCount = UnityEngine.Random.Range(1, 3);
        //for(int i=0;i<missionCount;i++)
        //{
        //    missonTimers[i].gameObject.SetActive(true);
        //    missonTimers[i].Selected();
        //}
        missionCount = 1;
        missonTimers[0].gameObject.SetActive(true);
        missonTimers[0].Selected();
    }

    public void IsAllGameEnd()
    {
        claerCount++;
        if(claerCount==missionCount)
        {
            claerCount = 0;
            nowMissionState = false;
        }
    }
}
