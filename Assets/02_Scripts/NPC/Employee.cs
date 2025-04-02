using UnityEngine;
using Random = System.Random;

public class Employee : MonoBehaviour
{
    /// <summary>
    /// 직원의 상태머신을 구성하는 상태의 집합
    /// </summary>
    public EmployeeStates EmployeeStates { get; private set; }
    /// <summary>
    /// 직원의 상태머신
    /// </summary>
    public NPCStateMachine NPCStateMachine { get; private set; }
    /// <summary>
    /// 직원의 스탯 정보
    /// </summary>
    public EmployData Data { get => EmployeeStates.EmployData; }
    
    private SpriteRenderer spriteRenderer;
    private Color missionColor;
    private Color defaultColor;
    
    private void Start()
    {
        EmployeeStates = GetComponent<EmployeeStates>();
        NPCStateMachine = GetComponent<NPCStateMachine>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        missionColor = new  Color(1, 190/255f, 190/255f, 1f);
        NPCManager.Instance.IdleEmployees.Enqueue(this);
    }
    
    /// <summary>
    /// 미션 할당 시 색깔을 변경
    /// </summary>
    public void ChangeToMissionColor()
    {
        spriteRenderer.color = missionColor;
    }
    
    /// <summary>
    /// 미션 할당 해제 시 색깔을 변경
    /// </summary>
    public void ChangeToDefaultColor()
    {
        spriteRenderer.color = defaultColor;
    }
    
    /// <summary>
    /// NPC에 미션을 할당, 수락 확률에 따라 NPCStateMachine 또는 null을 반환
    /// </summary>
    /// <param name="missionTimer">할당할 미션</param>
    public Employee AssignMission(MissionTimer missionTimer)
    {
        if (NPCStateMachine.CurrentNPCState != NPCStateMachine.npcIdleState)
        {
            //대기 상태가 아닐 경우 수락하지 않음
            NPCManager.Instance.IdleEmployees.Enqueue(this);
            return  null;
        }
        
        Random random = new Random();
        int acceptRate = random.Next(0, 100);
        //Sincerity에 따라 미션 수락 여부 결정(100이면 무조건 수락)
        if (acceptRate >= EmployeeStates.EmployData.Sincerity)
        {
            //Sincerity보다 acceptRate가 더 클 경우 수락하지 않음
            NPCManager.Instance.IdleEmployees.Enqueue(this);
            return null;
        }
        //미션 상태로 변경하고 해당 위치로 이동하도록 함
        NPCStateMachine.ChangeState(NPCStateMachine.npcMissionState);
        Vector3 targetPos = new Vector3(missionTimer.transform.position.x, missionTimer.transform.position.y, 0);
        NPCStateMachine.CurrentNPCState.TargetDestination = targetPos;
        return this;
    }
    
    /// <summary>
    /// 미션 할당이 해제되었을 때 대기 상태로 바꾸고 대기 직원 Queue에 추가
    /// </summary>
    public void QuitMission()
    {
        NPCManager.Instance.IdleEmployees.Enqueue(this);
        NPCStateMachine.ChangeState(NPCStateMachine.npcIdleState);
    }
    
}
