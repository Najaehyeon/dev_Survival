using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class EmployeeStates : StateSet
{
    public override NPCBaseState IdleState { get; set; }
    public override NPCBaseState RestState { get; set; }
    public override NPCBaseState MissionState { get; set; }

    [Header("직원 스탯 정보")] 
    public EmployData EmployData;
    
    public override void Init()
    {
        IdleState = new EmployeeIdleState(stateMachine);
        RestState = new EmployeeRestState(stateMachine);
        MissionState = new EmployeeMissionState(stateMachine);
    }
}
public class EmployeeIdleState : NPCBaseState
{
    float timeBetweenResetTarget = 10f;
    float passedTime;
    
    Vector3 prevTargetDestination = Vector3.zero;
    
    public EmployeeIdleState(NPCStateMachine stateMachine) : base(stateMachine)
    {
        destinations = NPCStateMachine.stateSet.IdleDestinationSet.DestinationSet;
    }

    public override void Enter()
    {
        NPCStateMachine.Controller.ChangeMoveSpeed(1f);
        TargetDestination = destinations[Random.Range(0, destinations.Length)];
        EmployeeManager.Instance.IdleEmployees.Enqueue(NPCStateMachine);
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        if(NPCStateMachine.HasMission)
            NPCStateMachine.ChangeState(NPCStateMachine.npcMissionState);
        else if(NPCStateMachine.StressLevel >= NPCStateMachine.MaxStress)
        {
            NPCStateMachine.ChangeState(NPCStateMachine.npcRestState);
        }
        else
        {
            //MissionManager에 의해 미션이 할당 되었을 때 배회를 멈추고 미션 장소로 이동
            SetRandomDestination();
        }
    }
    
    void SetRandomDestination()
    {
        if(passedTime > timeBetweenResetTarget)
        {
            Debug.Log("Set Random Destination");

            if(prevTargetDestination != Vector3.zero)
            {
                do
                {
                    TargetDestination = destinations[Random.Range(0, destinations.Length)];
                }
                while (TargetDestination == prevTargetDestination);
                
            }
            else
            {
                TargetDestination = destinations[Random.Range(0, destinations.Length)];
            }

            prevTargetDestination = TargetDestination;
            passedTime = 0f;
        }
        else
        {
            passedTime += Time.deltaTime;
        }
    }
}

public class EmployeeMissionState : NPCBaseState
{
    EmployData employData;
    
    public EmployeeMissionState(NPCStateMachine stateMachine) : base(stateMachine)
    {
        EmployeeStates employeeStates = stateMachine.stateSet as EmployeeStates;
        employData = employeeStates.EmployData;
    }
    
    public override void Enter()
    {
        Debug.Log("Employee Mission Enter");
        NPCStateMachine.Controller.ChangeMoveSpeed(5f);
    }
    public override void Exit()
    {
        
    }

    public override void Update()
    {
        
    }

    public override void OnMission(Object obj = null)
    {
        MissionTimer missionTimer = obj as MissionTimer;
        //미션 타이머의 직원 전용 해제 함수를 실행
        missionTimer.NPCInterection(NPCStateMachine);
    }
}

public class EmployeeRestState : NPCBaseState
{
    private float restTime = 30f;
    private float passedTime;
    
    public EmployeeRestState(NPCStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public override void Enter()
    {
        NPCStateMachine.Controller.ChangeMoveSpeed(5f);
    }
    public override void Exit()
    {
        NPCStateMachine.ResetStress();
    }

    public override void Update()
    {
        if (passedTime >= restTime)
        {
            StateMachine.ChangeState(NPCStateMachine.npcRestState);
        }
        else
        {
            passedTime += Time.deltaTime;
        }
    }
    
}
