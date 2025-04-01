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
    //MissionTimer를 통해 연결
    //NPC에 미션을 할당, Stat에 따른 수락 여부
    /// <summary>
    /// NPC에 미션을 할당, 수락 확률에 따라 NPCStateMachine 또는 null을 반환
    /// </summary>
    /// <param name="missionTimer">할당할 미션</param>
    public NPCStateMachine AssignMission(Mission mission)
    {
        if(CurrentNPCState != npcIdleState) return  null;
        
        EmployeeStates employeeStates = stateSet as EmployeeStates;
        
        Random random = new Random();
        
        int acceptRate = random.Next(0, 100);
        if (acceptRate < employeeStates.EmployData.Sincerity)
        {
            ChangeState(npcIdleState);
            return null;
        }
        
        Debug.Log("Receive mission");
        HasMission = true;
        CurrentNPCState.TargetDestination = mission.transform.position;
        return this;
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
    
    /// <summary>
    /// 미션 할당이 해제되었을 때
    /// </summary>
    public void QuitMission()
    {
        HasMission = false;
        ChangeState(npcIdleState);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        MissionTimer currentMissionTimer;
        if (other.TryGetComponent(out currentMissionTimer))
        {
            //고양이는 미션 지점에 도착했을 때 고양이 미션 타이머를 가동
            //OnMission -> 고양이 문제해결 미션이 시작
            //직원은 미션 지점에 도착했을 때 미션 타이머를 해결
            //OnMission -> 배정된 미션을 해결 (미션 타이머에서 직원 전용 함수)
            CurrentNPCState.OnMission(currentMissionTimer);
        }
    }
}
