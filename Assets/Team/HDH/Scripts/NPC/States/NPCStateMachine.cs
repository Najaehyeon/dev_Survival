using System;
using UnityEngine;
public class NPCStateMachine : BaseStateMachine
{
    StateSet stateSet;

    public NPCBaseState CurrentNPCState { get => CurrentState as NPCBaseState; }

    public NPCBaseState npcIdleState { get; private set; }
    public NPCBaseState npcMissionState { get; private set; }
    public NPCBaseState npcRestState { get; private set; }

    public NPCController Controller { get; private set; }

    public override void Init()
    {
        Controller = GetComponent<NPCController>();
        stateSet = GetComponent<StateSet>();
        stateSet.Init();
        
        npcIdleState = stateSet.IdleState;
        npcMissionState = stateSet.MissionState;
        npcRestState = stateSet.RestState;

        ChangeState(npcIdleState);
    }
}
