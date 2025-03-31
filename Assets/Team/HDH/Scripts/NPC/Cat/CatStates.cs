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
    float timeBetweenResetTarget = 5f;
    float passedTime;
    Vector3 prevTargetDestination;

    public CatIdleState(NPCStateMachine stateMachine) : base(stateMachine)
    {
        destinations = NPCStateMachine.stateSet.IdleDestinationSet.DestinationSet;
    }

    public override void Enter()
    {
        Debug.Log("CatIdle");
        base.Enter();
        NPCStateMachine.Controller.ChangeMoveSpeed(5f);
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

public class CatMissionState : NPCBaseState
{
    public CatMissionState(NPCStateMachine stateMachine) : base(stateMachine)
    {
        destinations = NPCStateMachine.stateSet.MissionDestinationSet.DestinationSet;
    }

    public override void Enter()
    {
        Debug.Log("CatMission");
        NPCStateMachine.Controller.ChangeMoveSpeed(10f);
        NPCStateMachine.AddStress(10f);
        //책상 위치 중 하나를 정해 이동
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
        
        missionTimer.OnGameStart();
    }
}

public class CatRestState : NPCBaseState
{
    private float restTime = 30f;
    private float timer;
    
    public CatRestState(NPCStateMachine stateMachine) : base(stateMachine)
    {
        destinations = NPCStateMachine.stateSet.RestDestinationSet.DestinationSet;
    }

    public override void Enter()
    {
        base.Enter();
        NPCStateMachine.Controller.ChangeMoveSpeed(10f);
        timer = 0;

    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        InRest();
        
        if(NPCStateMachine.IsRestComplete)
            NPCStateMachine.ChangeState(NPCStateMachine.npcIdleState);
    }

    private void InRest()
    {
        if (timer < restTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            NPCStateMachine.ResetStress();
        }
    }
}
