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
        //NPC를 미션 상태로 변경
        stateMachine.ChangeState(stateMachine.npcMissionState);
        //CatNPC에게 임의의 목적지를 할당
        stateMachine.CurrentNPCState.TargetDestination = catMissionTimers[UnityEngine.Random.Range(0, catMissionTimers.Length)].transform.position;
    }
    
    
}
