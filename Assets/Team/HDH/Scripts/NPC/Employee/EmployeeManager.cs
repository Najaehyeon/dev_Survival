using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EmployeeManager : Singleton<EmployeeManager>
{
    //NPC 업무 할당을 위한 Queue
    [field: SerializeField] public Queue<Employee> IdleEmployees = new Queue<Employee>();
    
    //같은 스프라이트에도 여러 스탯이 가능
    //스탯이 SO에 있으니
    //상점에서도 인덱스를 가지고 있고 NPC 매니저에
    
    //데이터에는 스탯 정보랑 인덱스, 이름
    //스프라이트랑 생성할 때 게임오브젝트가 필요한데
    //데이터에 프리팹까지 할당
    
    //상점에서 인덱스를 받기만 하면 됨
    
    [SerializeField] private List<EmployData> employeeDataList = new List<EmployData>();
    // 고용된 직원 리스트
    public List<GameObject> hiredEmployees = new List<GameObject>();
    
    public Transform employeeSpawnPoint;
    
    [SerializeField] private StateDestinationSet[] destinationSets = new StateDestinationSet[3];
    
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
        employeeObject.AddComponent<EmployeeStates>().EmployData = employeeData;
        employeeObject.GetComponent<EmployeeStates>().IdleDestinationSet = destinationSets[0];
        employeeObject.GetComponent<EmployeeStates>().MissionDestinationSet = destinationSets[1];
        employeeObject.GetComponent<EmployeeStates>().RestDestinationSet = destinationSets[2];
        employeeObject.AddComponent<NPCController>();
        employeeObject.AddComponent<NPCStateMachine>();
    }
    
    /// <summary>
    /// 게임 시작시 직원 위치를 초기화
    /// </summary>
    public void ResetEmployeesPosition()
    {
        foreach (GameObject employee in hiredEmployees)
        {
            employee.transform.position = employeeSpawnPoint.position;
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
