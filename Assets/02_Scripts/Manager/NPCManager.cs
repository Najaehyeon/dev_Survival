using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NPCManager : Singleton<NPCManager>
{
    //NPC 업무 할당을 위한 Queue
    public Queue<Employee> IdleEmployees = new Queue<Employee>();
    
    [SerializeField] private List<EmployData> employeeDataList = new List<EmployData>();
    // 고용된 직원 리스트
    public List<GameObject> hiredEmployees = new List<GameObject>();
    
    public Transform employeeSpawnPoint;
    
    [SerializeField] private StateDestinationData[] destinationSets = new StateDestinationData[3];

    public GameObject HireEmployee(int index)
    {
        if (index >= 0 && index < employeeDataList.Count)
        {
            EmployData employeeData = employeeDataList[index];
            GameObject employeeObject = Instantiate(employeeData.EmployeePrefab, employeeSpawnPoint.position, Quaternion.identity);

            // 필요한 스크립트 추가 및 초기화
            InitializeEmployee(employeeObject, employeeData);
            
            // 고용된 직원 리스트에 추가
            hiredEmployees.Add(employeeObject);
            
            employeeObject.SetActive(false);
            
            return employeeObject;
        }
        else
        {
            Debug.LogError("Invalid employee index: " + index);
            return null;
        }
    }

    private void InitializeEmployee(GameObject employeeObject, EmployData employeeData)
    {
        employeeObject.AddComponent<Employee>();
        employeeObject.AddComponent<NavMeshAgent>();
        employeeObject.AddComponent<AnimationHandler>();
        employeeObject.AddComponent<EmployeeStates>().EmployData = employeeData;
        employeeObject.GetComponent<EmployeeStates>().idleDestinationData = destinationSets[0];
        employeeObject.GetComponent<EmployeeStates>().missionDestinationData = destinationSets[1];
        employeeObject.GetComponent<EmployeeStates>().restDestinationData = destinationSets[2];
        employeeObject.AddComponent<NPCController>();
        employeeObject.AddComponent<NPCStateMachine>();
    }
    
    /// <summary>
    /// 게임 시작시 직원 위치를 초기화
    /// </summary>
    public void ActiveEmployees()
    {
        foreach (GameObject employee in hiredEmployees)
        {
            employee.gameObject.SetActive(true);
            employee.transform.position = employeeSpawnPoint.position;
        }
    }

    public void InactiveEmployees()
    {
        foreach (GameObject employee in hiredEmployees)
        {
            employee.gameObject.SetActive(false);
        }
    }
    
    /// <summary>
    /// 고용된 직원을 해고
    /// </summary>
    /// <param name="employee">해고할 직원</param>
    public void FireEmployee(GameObject employee)
    {
        if (hiredEmployees.Contains(employee))
        {
            hiredEmployees.Remove(employee);
            Destroy(employee);
        }
        else
        {
            Debug.LogError("Employee not found in hired list: " + employee.name);
        }
    }
    
    
}
