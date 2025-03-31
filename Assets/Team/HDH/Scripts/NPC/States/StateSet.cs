using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSet : MonoBehaviour
{
    protected NPCStateMachine stateMachine;
    
    [SerializeField] public StateDestinationSet IdleDestinationSet;
    [SerializeField] public StateDestinationSet MissionDestinationSet;
    [SerializeField] public StateDestinationSet RestDestinationSet;
    private void Start()
    {
        stateMachine = GetComponent<NPCStateMachine>();
    }

    public virtual NPCBaseState IdleState { get; set; }
    public virtual NPCBaseState MissionState { get; set; }
    public virtual NPCBaseState RestState { get; set; }

    public virtual void Init() { }
}
