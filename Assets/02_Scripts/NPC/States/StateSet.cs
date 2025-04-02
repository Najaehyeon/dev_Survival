using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StateSet : MonoBehaviour
{
    protected NPCStateMachine stateMachine;
    
    [FormerlySerializedAs("IdleDestinationSet")] [SerializeField] public StateDestinationData idleDestinationData;
    [FormerlySerializedAs("MissionDestinationSet")] [SerializeField] public StateDestinationData missionDestinationData;
    [FormerlySerializedAs("RestDestinationSet")] [SerializeField] public StateDestinationData restDestinationData;
    private void Start()
    {
        stateMachine = GetComponent<NPCStateMachine>();
    }

    public virtual NPCBaseState IdleState { get; set; }
    public virtual NPCBaseState MissionState { get; set; }
    public virtual NPCBaseState RestState { get; set; }

    public virtual void Init() { }
}
