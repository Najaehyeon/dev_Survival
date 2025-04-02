using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class NPCManager : Singleton<NPCManager>
{
    //NPC 업무 할당을 위한 Queue
    public Queue<Employee> IdleEmployees = new Queue<Employee>();
    
    [Header("직원 정보")]
    [SerializeField] private List<EmployData> employeeDataList = new List<EmployData>();
    // 고용된 직원 리스트
    public List<Employee> hiredEmployees = new List<Employee>();
    
    [FormerlySerializedAs("employeeSpawnPoint")] public Transform SpawnPoint;
    
    [SerializeField] private StateDestinationData[] destinationSets = new StateDestinationData[3];

    [Header("고양이 정보")] 
    [SerializeField] private GameObject catPrefab;
    
    [Header("강아지 정보")]
    [SerializeField] private GameObject dogPrefab;

    public GameObject HireEmployee(int index)
    {
        if (index >= 0 && index < employeeDataList.Count)
        {
            EmployData employeeData = employeeDataList[index];
            GameObject employeeObject = Instantiate(employeeData.EmployeePrefab, SpawnPoint.position, Quaternion.identity);

            // 필요한 스크립트 추가 및 초기화
            InitializeEmployee(employeeObject, employeeData);
            
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
        hiredEmployees.Add(employeeObject.AddComponent<Employee>());
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
        foreach (Employee employee in hiredEmployees)
        {
            employee.gameObject.SetActive(true);
            employee.transform.position = SpawnPoint.position;
        }
    }

    public void InactiveEmployees()
    {
        foreach (Employee employee in hiredEmployees)
        {
            employee.npcStateMachine.ResetStress();
            employee.gameObject.SetActive(false);
        }
    }
    
    /// <summary>
    /// 고용된 직원을 해고
    /// </summary>
    /// <param name="employee">해고할 직원</param>
    public void FireEmployee(int index)
    {
        if (index >= 0 && index < employeeDataList.Count)
        {
            EmployData employeeData = employeeDataList[index];
            
            Employee fireEmployee = null;
        
            foreach (Employee employee in hiredEmployees)
            {
                if (employee.Data == employeeData)
                {
                    fireEmployee = employee;
                    break;
                }
            }

            if (hiredEmployees.Contains(fireEmployee))
            {
                hiredEmployees.Remove(fireEmployee);
                Destroy(fireEmployee);
            }
            else
            {
                Debug.LogError("Employee not found in hired list");
            }
        }
        else
        {
            Debug.LogError("Index out of range");
        }
        
    }

    public void SpawnCat()
    {
        Instantiate(catPrefab, SpawnPoint.position, Quaternion.identity);
    }

    public void SpawnDog()
    {
        Instantiate(dogPrefab, SpawnPoint.position, Quaternion.identity);
    }
    
    
}
