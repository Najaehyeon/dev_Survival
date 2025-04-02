using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//리팩토링할때 MissionHandler로 바꿔줄것
public class MissionTimer:MonoBehaviour
{
    private Mission missionInstance;
    public Mission mission;

    public float timeOut = 20f;
    public Image radialFill;
    public Color normalColor = Color.white;
    public Color warningColor = Color.red;
    [SerializeField] private bool gameStart;

    //NPC정보 가지고있는애로 변경
    public Employee? target;
    public virtual void Update()
    {
        if (!gameStart)
        {
            if (timeOut > 0)
            {
                timeOut -= Time.deltaTime;
                UpdateFillAmount();
            }
            else
            {
                TimeOut();
            }
        }
    }

    /// <summary>
    /// 미션 컨트롤러에서 지정되면 타이머 활성화
    /// </summary>
    public void Selected()
    {
        gameStart= false;
        timeOut = 20f;
        gameObject.SetActive(true);
        MissionManager.Instance.SelectedMissions.Add(this);
        HireNPC();
    }

    /// <summary>
    /// 활성화시 npc에게 미션 부여
    /// </summary>
    public void HireNPC()
    {
        if (EmployeeManager.Instance.IdleEmployees.Count == 0) return;
        target = EmployeeManager.Instance.IdleEmployees.Dequeue().AssignMission(this);
    }


    /// <summary>
    /// 플레이어가 상호작용하면 타이머에 부여된 미션 실행
    /// </summary>
    public virtual void OnGameStart()
    {
        gameStart = true;
        missionInstance = Instantiate(mission, MissionManager.Instance.controller.canvas.transform);
        MissionManager.Instance.RemoveMission(this);
        if (target !=null)target.QuitMission();
        gameObject.SetActive(false);
        GameManager.Instance.isMissionInProgress = true;
    }
    /// <summary>
    /// NPC가 상호작용하면 호출 타이머 종료
    /// </summary>
    /// <param name="interect"></param>
    public void NPCInterection(Employee interect)
    {
        if (interect == target)
        {
            Debug.Log("미션 전달");
            MissionManager.Instance.RemoveMission(this);
            gameObject.SetActive(false);
            
            //target의 스테이터스에따라 스코어랑 스트레스 반환
            CalculateScore(interect);
            MissionManager.Instance.controller.IsAllGameEnd();
            Debug.Log("NPC와 소통");

        }
    }
    /// <summary>
    /// 제한시간안에 상호작용 되지 않으면 호출
    /// </summary>
    public virtual void TimeOut()
    {
        gameStart = true;
        GameManager.Instance.ChangeStress(10);
        MissionManager.Instance.RemoveMission(this);
        if (target != null) target.QuitMission();
        MissionManager.Instance.controller.IsAllGameEnd();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 스테이트전환시 타이머 종료
    /// </summary>
    public void IsDayEnd()
    {
        gameStart = true;
        if (target != null) target.QuitMission();
        gameObject.SetActive(false);
    }
    private void UpdateFillAmount()
    {
        radialFill.fillAmount =1-(timeOut / 20f);

        if (timeOut <= 5f)
        {
            radialFill.color = warningColor;
        }
        else
        {
            radialFill.color = normalColor;
        }
    }

    private void CalculateScore(Employee interect)
    {
        int success = interect.Data.Ability;
        if(success>Random.Range(1,101))
        {
            GameManager.Instance.ChangeScore(3);
        }
        else
        {
            GameManager.Instance.ChangeStress(5);
        }
    }
}
