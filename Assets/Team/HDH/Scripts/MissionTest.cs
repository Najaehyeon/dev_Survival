using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTest : MonoBehaviour
{
    MissionTimer missionTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        missionTimer = GetComponent<MissionTimer>();
        StartCoroutine(StartMission());
    }

    IEnumerator StartMission()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("직원 생성");
        EmployeeManager.Instance.HireEmployee(0);
        //EmployeeManager.Instance.SpwanHiredEmployee();
        
        yield return new WaitForSeconds(5f);
        Debug.Log("미션 할당");
        EmployeeManager.Instance.IdleEmployees.Dequeue().AssignMission(missionTimer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
