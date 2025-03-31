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

        Debug.Log("Mission Assign");
        EmployeeManager.Instance.employees[0].AssignMission(this);
    }
    public virtual void OnGameStart()
    {
        gameStart = true;
        missionInstance = Instantiate(mission, MissionManager.Instance.controller.canvas.transform);
        gameObject.SetActive(false);
    }
    public virtual void TimeOut()
    {
        gameStart = true;
        // 점수, 스트레스 줘야함
        MissionManager.Instance.controller.IsAllGameEnd();
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
