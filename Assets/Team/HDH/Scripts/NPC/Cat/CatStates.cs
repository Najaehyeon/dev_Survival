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
    public CatMissionState(NPCStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("CatMission");
        //미션 매니저에서 장소를 입력 받아 TargetDestination에 할당
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
    public CatRestState(NPCStateMachine stateMachine) : base(stateMachine)
    {
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
