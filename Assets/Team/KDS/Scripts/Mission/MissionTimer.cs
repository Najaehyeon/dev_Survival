using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    public void Selected()
    {
        gameStart= false;
        timeOut = 20f;
        gameObject.SetActive(true);
        MissionManager.Instance.SelectedMissions.Add(this);
        HireNPC();
    }

    public void HireNPC()
    {

            Debug.Log("고용중");
            //아이덜 스테이트인 npc 리스트(NPCManager.intance.IdalNPCs) 순회하면서 npc에게 AssignMission(missionTimer);
            //missionTimer.mission.target = 

    }
    public virtual void OnGameStart()
    {
        gameStart = true;
        missionInstance = Instantiate(mission, MissionManager.Instance.controller.canvas.transform);
        MissionManager.Instance.SelectedMissions.Remove(this);
        if (mission.target!=null)mission.target.QuitMission();
        gameObject.SetActive(false);
        GameManager.Instance.isMissionInProgress = true;
    }
    public virtual void TimeOut()
    {
        gameStart = true;
        GameManager.Instance.ChangeStress(10);
        MissionManager.Instance.SelectedMissions.Remove(this);
        if (mission.target != null) mission.target.QuitMission();
        MissionManager.Instance.controller.IsAllGameEnd();
        gameObject.SetActive(false);
    }
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

    public void NPCInterection(NPCStateMachine interect)
    {
        MissionManager.Instance.SelectedMissions.Remove(this);
        mission.NPCInterection(interect);
        gameObject.SetActive(false);
    }
}
