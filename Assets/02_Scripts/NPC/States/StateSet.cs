using UnityEngine;

public class StateSet : MonoBehaviour
{
    protected NPCStateMachine stateMachine;
    
    [SerializeField] public StateDestinationData idleDestinationData;
    [SerializeField] public StateDestinationData missionDestinationData;
    [SerializeField] public StateDestinationData restDestinationData;
    
    public virtual NPCBaseState IdleState { get; set; }
    public virtual NPCBaseState MissionState { get; set; }
    public virtual NPCBaseState RestState { get; set; }

    public virtual void Init(NPCStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
}
