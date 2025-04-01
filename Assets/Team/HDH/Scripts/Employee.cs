using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Employee : MonoBehaviour
{
    private EmployeeStates employeeStates;
    private NPCStateMachine npcStateMachine;

    public EmployData Data { get => employeeStates.EmployData; }

    private void Start()
    {
        employeeStates = GetComponent<EmployeeStates>();
        npcStateMachine = GetComponent<NPCStateMachine>();
        EmployeeManager.Instance.IdleEmployees.Enqueue(this);
    }
    
    //MissionTimer를 통해 연결
    //NPC에 미션을 할당, Stat에 따른 수락 여부
    /// <summary>
    /// NPC에 미션을 할당, 수락 확률에 따라 NPCStateMachine 또는 null을 반환
    /// </summary>
    /// <param name="missionTimer">할당할 미션</param>
    public Employee AssignMission(MissionTimer missionTimer)
    {
        if(npcStateMachine.CurrentNPCState != npcStateMachine.npcIdleState) return  null;
        
        Random random = new Random();
        
        // int acceptRate = random.Next(0, 100);
        // if (acceptRate <= employeeStates.EmployData.Sincerity)
        // {
        //     Debug.Log("미션 거절");
        //     QuitMission();
        //     return null;
        // }
        
        Debug.Log("Receive mission");
        npcStateMachine.ChangeState(npcStateMachine.npcMissionState);
        Vector3 targetPos = new Vector3(missionTimer.transform.position.x, missionTimer.transform.position.y, 0);
        npcStateMachine.CurrentNPCState.TargetDestination = targetPos;
        return this;
    }
    
    /// <summary>
    /// 미션 할당이 해제되었을 때
    /// </summary>
    public void QuitMission()
    {
        EmployeeManager.Instance.IdleEmployees.Enqueue(this);
        npcStateMachine.ChangeState(npcStateMachine.npcIdleState);
    }
    
}
