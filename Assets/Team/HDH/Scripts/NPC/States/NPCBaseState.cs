using UnityEngine;

public abstract class NPCBaseState : BaseState
{
    protected NPCStateMachine NPCStateMachine;

    protected StateSet StateSet;
    
    protected Vector3[] destinations;
    public Vector3 TargetDestination;

    protected NPCBaseState(NPCStateMachine stateMachine) : base(stateMachine)
    {
        NPCStateMachine = stateMachine;
    }

    public virtual void OnMission(Object obj = null) { }
}
