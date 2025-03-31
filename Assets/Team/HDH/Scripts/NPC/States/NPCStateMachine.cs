using System;
using UnityEngine;
public class NPCStateMachine : BaseStateMachine
{
    public StateSet stateSet;

    public NPCBaseState CurrentNPCState { get => CurrentState as NPCBaseState; }

    public NPCBaseState npcIdleState { get; private set; }
    public NPCBaseState npcMissionState { get; private set; }
    public NPCBaseState npcRestState { get; private set; }

    public NPCController Controller { get; private set; }
    
    public float StressLevel { get; private set; }
    public float MaxStress = 100f;
    public bool HasMission { get; private set; }
    public bool IsRestComplete { get; private set; }

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

    public void AssignMission(MissionTimer missionTimer)
    {
        if(CurrentNPCState != npcIdleState) return;
        HasMission = true;
        CurrentNPCState.TargetDestination = missionTimer.transform.position;
    }

    public void AddStress(float value)
    {
        StressLevel = Mathf.Min(StressLevel + value, MaxStress);
    }

    public void ResetStress()
    {
        StressLevel = 0;
        IsRestComplete = true;
    }

    public void StartMissionTimer(MissionTimer missionTimer)
    {
        missionTimer.gameObject.SetActive(true);
        missionTimer.Selected();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        MissionTimer currentMissionTimer;
        if (other.TryGetComponent(out currentMissionTimer))
        {
            //고양이는 미션 지점에 도착했을 때 고양이 미션 타이머를 가동
            //직원은 미션 지점에 도착했을 때 미션 타이머를 해결
            CurrentNPCState.OnMission(currentMissionTimer);
        }
    }
}
