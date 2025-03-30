using UnityEngine;

public abstract class NPCBaseState : BaseState
{
    protected NPCStateMachine NPCStateMachine;

    protected StateSet StateSet;

    public Vector3 TargetDestination;

    protected NPCBaseState(NPCStateMachine stateMachine) : base(stateMachine)
    {
        NPCStateMachine = stateMachine;
    }

    public virtual void OnMission() { }
}
