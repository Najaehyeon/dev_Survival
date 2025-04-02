using UnityEngine;

public abstract class NPCBaseState : BaseState
{
    protected NPCStateMachine NPCStateMachine;

    protected StateSet StateSet;
    
    protected Vector3[] destinations;
    public Vector3 TargetDestination;

    protected readonly float idleSpeed = 1f;
    protected readonly float missionSpeed = 3f;
    protected readonly float restSpeed = 0.5f;
    

    protected NPCBaseState(NPCStateMachine stateMachine) : base(stateMachine)
    {
        NPCStateMachine = stateMachine;
    }

    public virtual void OnMission(Object obj = null) { }
    
    protected float passedTime;
    Vector3 prevTargetDestination = Vector3.zero;
    
    protected void SetRandomDestination(float timeBetweenResetTarget)
    {
        if(passedTime > timeBetweenResetTarget)
        {
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
