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
        //CatNPC에게 임의의 목적지를 할당
    }

    public void EndMission()
    {
        stateMachine.ChangeState(stateMachine.npcIdleState);
    }
    
    
}
