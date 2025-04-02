using UnityEngine;

public class CatStates : StateSet
{
    public override NPCBaseState IdleState { get; set; }
    public override NPCBaseState RestState { get; set; }
    public override NPCBaseState MissionState { get; set; }

    public override void Init(NPCStateMachine npcStateMachine)
    {
        base.Init(npcStateMachine);
        IdleState = new CatIdleState(stateMachine);
        RestState = new CatMissionState(stateMachine);
        MissionState = new CatRestState(stateMachine);
    }
}

public class CatIdleState : NPCBaseState
{
    float timeBetweenResetTarget = 10f;

    public CatIdleState(NPCStateMachine stateMachine) : base(stateMachine)
    {
        destinations = NPCStateMachine.StateSet.idleDestinationData.DestinationSet;
    }

    public override void Enter()
    {
        NPCStateMachine.Controller.ChangeMoveSpeed(idleSpeed);
        TargetDestination = destinations[Random.Range(0, destinations.Length)];
    }

    public override void Exit()
    {
        passedTime = 0f;
    }

    public override void Update()
    { 
        SetRandomDestination(timeBetweenResetTarget);
    }
}

public class CatMissionState : NPCBaseState
{
    public CatMissionState(NPCStateMachine stateMachine) : base(stateMachine)
    {
        destinations = NPCStateMachine.StateSet.missionDestinationData.DestinationSet;
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
    private float restTime = 10f;
    
    public CatRestState(NPCStateMachine stateMachine) : base(stateMachine)
    {
        destinations = NPCStateMachine.StateSet.restDestinationData.DestinationSet;
    }

    public override void Enter()
    {
        NPCStateMachine.Controller.ChangeMoveSpeed(restSpeed);
        passedTime = 0;

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
        if (passedTime < restTime)
        {
            passedTime += Time.deltaTime;
        }
        else
        {
            NPCStateMachine.ResetStress();
        }
    }
}
