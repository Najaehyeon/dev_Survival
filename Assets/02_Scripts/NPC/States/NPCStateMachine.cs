using UnityEngine;

public class NPCStateMachine : BaseStateMachine
{
    /// <summary>
    /// 현재 상태
    /// </summary>
    public NPCBaseState CurrentNPCState { get => CurrentState as NPCBaseState; }
    /// <summary>
    /// NPC가 가질 수 있는 상태들의 집합
    /// </summary>
    public StateSet StateSet { get; private set; }

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
        StateSet = GetComponent<StateSet>();
        StateSet.Init(this);
        
        npcIdleState = StateSet.IdleState;
        npcMissionState = StateSet.MissionState;
        npcRestState = StateSet.RestState;

        ChangeState(npcIdleState);
    }
    /// <summary>
    /// NPC의 스트레스 값을 추가
    /// </summary>
    /// <param name="value">추가할 스트레스 값</param>
    public void AddStress(float value)
    {
        StressLevel = Mathf.Min(StressLevel + value, MaxStress);
    }
    
    /// <summary>
    /// NPC의 스트레스를 초기화
    /// </summary>
    public void ResetStress()
    {
        StressLevel = 0;
        IsRestComplete = true;
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
