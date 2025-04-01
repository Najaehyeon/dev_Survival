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
            Debug.Log("고용중");
            //아이덜 스테이트인 npc 리스트(NPCManager.intance.IdalNPCs) 순회하면서 npc에게 AssignMission(missionTimer);
            //missionTimer.mission.target = 
    }


    /// <summary>
    /// 플레이어가 상호작용하면 타이머에 부여된 미션 실행
    /// </summary>
    public virtual void OnGameStart()
    {
        gameStart = true;
        missionInstance = Instantiate(mission, MissionManager.Instance.controller.canvas.transform);
        MissionManager.Instance.SelectedMissions.Remove(this);
        if (mission.target!=null)mission.target.QuitMission();
        gameObject.SetActive(false);
        GameManager.Instance.isMissionInProgress = true;
    }
    /// <summary>
    /// NPC가 상호작용하면 호출 타이머 종료
    /// </summary>
    /// <param name="interect"></param>
    public void NPCInterection(NPCStateMachine interect)
    {
        MissionManager.Instance.SelectedMissions.Remove(this);
        mission.NPCInterection(interect);
        gameObject.SetActive(false);
    }
    /// <summary>
    /// 제한시간안에 상호작용 되지 않으면 호출
    /// </summary>
    public virtual void TimeOut()
    {
        gameStart = true;
        GameManager.Instance.ChangeStress(10);
        MissionManager.Instance.SelectedMissions.Remove(this);
        if (mission.target != null) mission.target.QuitMission();
        MissionManager.Instance.controller.IsAllGameEnd();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 스테이트전환시 타이머 종료
    /// </summary>
    public void IsDayEnd()
    {
        gameStart = true;
        MissionManager.Instance.SelectedMissions.Remove(this);
        if (mission.target != null) mission.target.QuitMission();
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


}
