using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogStates : StateSet
{
    public override NPCBaseState IdleState { get; set; }
    public override NPCBaseState RestState { get; set; }
    public override NPCBaseState MissionState { get; set; }

    public override void Init()
    {
        IdleState = new DogIdleState(stateMachine);
        RestState = new DogMissionState(stateMachine);
        MissionState = new DogRestState(stateMachine);
    }
}

public class  DogIdleState : NPCBaseState
{
    float timeBetweenResetTarget = 10f;
    
    public DogIdleState(NPCStateMachine stateMachine) : base(stateMachine)
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

public class DogMissionState : NPCBaseState
{
    
    
    public DogMissionState(NPCStateMachine stateMachine) : base(stateMachine)
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

public class DogRestState : NPCBaseState
{
    public DogRestState(NPCStateMachine stateMachine) : base(stateMachine)
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
