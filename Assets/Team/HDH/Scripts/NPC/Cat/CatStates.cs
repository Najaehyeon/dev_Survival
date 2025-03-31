using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStates : StateSet
{
    public override NPCBaseState IdleState { get; set; }
    public override NPCBaseState RestState { get; set; }
    public override NPCBaseState MissionState { get; set; }

    public override void Init()
    {
        IdleState = new CatIdleState(stateMachine);
        RestState = new CatMissionState(stateMachine);
        MissionState = new CatRestState(stateMachine);
    }
}

public class CatIdleState : NPCBaseState
{
    public Vector3[] IdleDestinations;
    float timeBetweenResetTarget = 5f;
    float passedTime;
    Vector3 prevTargetDestination;

    public CatIdleState(NPCStateMachine stateMachine) : base(stateMachine)
    {
        IdleDestinations = NPCStateMachine.Controller.IdleDestinationSet.DestinationSet;
    }

    public override void Enter()
    {
        Debug.Log("CatIdle");
        TargetDestination = IdleDestinations[0];
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        //MissionManager에 의해 미션이 할당 되었을 때 배회를 멈추고 미션 장소로 이동
        
        SetRandomDestination();
            
    }

    void SetRandomDestination()
    {
        if(passedTime > timeBetweenResetTarget)
        {
            Debug.Log("Set Random Destination");

            if(prevTargetDestination != null)
            {
                do
                {
                    TargetDestination = IdleDestinations[Random.Range(0, IdleDestinations.Length)];
                }
                while (TargetDestination == prevTargetDestination);
                
            }
            else
            {
                TargetDestination = IdleDestinations[Random.Range(0, IdleDestinations.Length)];
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

public class CatMissionState : NPCBaseState
{
    public Vector3[] MissionDestinations;
    public CatMissionState(NPCStateMachine stateMachine) : base(stateMachine)
    {
        MissionDestinations = NPCStateMachine.Controller.MissionDestinationSet.DestinationSet;
    }

    public override void Enter()
    {
        Debug.Log("CatMission");
        //책상 위치 중 하나를 정해 이동
        TargetDestination = MissionDestinations[Random.Range(0, MissionDestinations.Length)];
        //도착한 이후의 애니메이션 등은 Controller에서 진행
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        
    }
}

public class CatRestState : NPCBaseState
{
    public Vector3[] RestDestinations;
    public CatRestState(NPCStateMachine stateMachine) : base(stateMachine)
    {
        RestDestinations = NPCStateMachine.Controller.RestDeaStateDestinationSet.DestinationSet;
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        
    }
}
