using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMission : Mission
{
    [SerializeField] MissionTimer[] catMissionTimers;
    private NPCStateMachine stateMachine;

    private void Start()
    {
        stateMachine = GetComponent<NPCStateMachine>();
        
        foreach (MissionTimer timer in catMissionTimers)
        {
            timer.mission = this;
        }
    }

    public void SelectCatMission()
    {
        stateMachine.CurrentNPCState.TargetDestination = catMissionTimers[UnityEngine.Random.Range(0, catMissionTimers.Length)].transform.position;
    }
    
    
}
