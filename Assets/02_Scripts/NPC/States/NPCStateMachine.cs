using System;
using UnityEngine;
using Random = System.Random;

public class NPCStateMachine : BaseStateMachine
{
    public StateSet stateSet;

    public NPCBaseState CurrentNPCState { get => CurrentState as NPCBaseState; }

    public NPCBaseState npcIdleState { get; private set; }
    public NPCBaseState npcMissionState { get; private set; }
    public NPCBaseState npcRestState { get; private set; }

    public NPCController Controller { get; private set; }
    
    [field: SerializeField] public float StressLevel { get; private set; }
    public float MaxStress = 100f;
    public bool IsRestComplete { get; private set; }

    public override void Init()
    {
        Controller = GetComponent<NPCController>();
        stateSet = GetComponent<StateSet>();
        stateSet.Init(this);
        
        npcIdleState = stateSet.IdleState;
        npcMissionState = stateSet.MissionState;
        npcRestState = stateSet.RestState;

        ChangeState(npcIdleState);
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
    
    /// <summary>
    /// 미션의 타이머를 시작
    /// </summary>
    /// <param name="missionTimer">타이머를 작동시킬 미션의 타이머</param>
    public void StartMissionTimer(MissionTimer missionTimer)
    {
        missionTimer.gameObject.SetActive(true);
        missionTimer.Selected();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CurrentNPCState != npcMissionState) return;
        
        if (other.TryGetComponent(out MissionTimer currentMissionTimer))
        {
            //고양이는 미션 지점에 도착했을 때 고양이 미션 타이머를 가동
            //OnMission -> 고양이 문제해결 미션이 시작
            //직원은 미션 지점에 도착했을 때 미션 타이머를 해결
            //OnMission -> 배정된 미션을 해결 (미션 타이머에서 직원 전용 함수)
            CurrentNPCState.OnMission(currentMissionTimer);
        }
    }

    public Employee GetEmployee()
    {
        return GetComponent<Employee>();
    }
}
