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

    public CatIdleState(NPCStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        Debug.Log("CatIdle");
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        NPCStateMachine.Controller.SetDestination(TargetDestination);
    }
}

public class CatMissionState : NPCBaseState
{
    public CatMissionState(NPCStateMachine stateMachine) : base(stateMachine)
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
