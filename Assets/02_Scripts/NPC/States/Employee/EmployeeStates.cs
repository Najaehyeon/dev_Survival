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
    
    public override void Init(NPCStateMachine npcStateMachine)
    {
        base.Init(npcStateMachine);
        IdleState = new EmployeeIdleState(stateMachine);
        RestState = new EmployeeRestState(stateMachine);
        MissionState = new EmployeeMissionState(stateMachine);
    }
}
public class EmployeeIdleState : NPCBaseState
{
    float timeBetweenResetTarget = 10f;
    
    public EmployeeIdleState(NPCStateMachine stateMachine) : base(stateMachine)
    {
        destinations = NPCStateMachine.stateSet.idleDestinationData.DestinationSet;
    }

    public override void Enter()
    {
        NPCStateMachine.Controller.ChangeMoveSpeed(1f);
        TargetDestination = destinations[Random.Range(0, destinations.Length)];
    }

    public override void Exit()
    {
        passedTime = 0f;
    }

    public override void Update()
    { 
        //MissionManager에 의해 미션이 할당 되었을 때 배회를 멈추고 미션 장소로 이동
        SetRandomDestination(timeBetweenResetTarget);
    }
}

public class EmployeeMissionState : NPCBaseState
{
    Employee employee;
    private float missionDelayTime = 1.5f;
    private bool onMission;
    
    public EmployeeMissionState(NPCStateMachine stateMachine) : base(stateMachine)
    {
        employee = NPCStateMachine.GetEmployee();
    }
    
    public override void Enter()
    {
        Debug.Log("미션 시작");
        NPCStateMachine.Controller.ChangeMoveSpeed(3f);
    }
    public override void Exit()
    {
        Debug.Log("미션 종료");
        onMission = false;
        passedTime = 0f;
    }

    public override void Update()
    {
        if (onMission)
        {
            passedTime += Time.deltaTime;
            
            if(passedTime > missionDelayTime)
                employee.QuitMission();
        }
    }

    public override void OnMission(Object obj = null)
    {
        MissionTimer missionTimer = obj as MissionTimer;
        //미션 타이머의 직원 전용 해제 함수를 실행
        Debug.Log( employee.gameObject.name + "Mission Enter");
        missionTimer.NPCInterection(employee);
        NPCStateMachine.AddStress(10 * employee.Data.StressControl);
        onMission = true;
    }
}

public class EmployeeRestState : NPCBaseState
{
    private float restTime = 10f;
    
    public EmployeeRestState(NPCStateMachine stateMachine) : base(stateMachine)
    {
        destinations =  NPCStateMachine.stateSet.restDestinationData.DestinationSet;
    }
    
    public override void Enter()
    {
        Debug.Log("Enter Rest");
        NPCStateMachine.Controller.ChangeMoveSpeed(0.5f);
        TargetDestination = destinations[0];
    }
    public override void Exit()
    {
        passedTime = 0f;
        NPCStateMachine.ResetStress();
    }

    public override void Update()
    {
        passedTime += Time.deltaTime;
        if (passedTime > restTime)
            StateMachine.ChangeState(NPCStateMachine.npcIdleState);

    }
    
}
