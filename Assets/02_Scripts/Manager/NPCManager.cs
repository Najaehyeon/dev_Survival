using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCManager : Singleton<NPCManager>
{
    /// <summary>
    /// 현재 대기 상태인 직원 Queue
    /// </summary>
    public Queue<Employee> IdleEmployees = new Queue<Employee>();
    
    [Header("직원 정보")]
    [SerializeField] private List<EmployData> employeeDataList = new List<EmployData>();
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private StateDestinationData[] destinationSets = new StateDestinationData[3];
    [SerializeField] private List<Employee> HiredEmployees = new List<Employee>();
    
    [Header("고양이 정보")] 
    [SerializeField] private GameObject catPrefab;
    
    [Header("강아지 정보")]
    [SerializeField] private GameObject dogPrefab;
    
    /// <summary>
    /// 상점에서 선택한 직원을 생성
    /// </summary>
    /// <param name="index">직원 데이터에서 생성할 직원에 대한 인덱스</param>
    /// <returns></returns>
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
        HiredEmployees.Add(employeeObject.AddComponent<Employee>());
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
    /// 게임 시작시 직원 오브젝트를 활성화 및 시작 위치로 초기화
    /// </summary>
    public void ActiveEmployees()
    {
        if(HiredEmployees.Count == 0) return;
        
        foreach (Employee employee in HiredEmployees)
        {
            employee.gameObject.SetActive(true);
            employee.transform.position = SpawnPoint.position;
        }
    }
    
    /// <summary>
    /// 게임 종료시 직원 오브젝트를 비활성화 및 스트레스를 초기화
    /// </summary>
    public void InactiveEmployees()
    {
        if(HiredEmployees.Count == 0) return;
        
        foreach (Employee employee in HiredEmployees)
        {
            employee.NPCStateMachine.ResetStress();
            employee.gameObject.SetActive(false);
        }
    }
    
    /// <summary>
    /// 고용 중인 직원을 삭제
    /// </summary>
    /// <param name="index">삭제할 직원의 인덱스 번호</param>
    public void FireEmployee(int index)
    {
        if (index >= 0 && index < employeeDataList.Count)
        {
            EmployData employeeData = employeeDataList[index];
            
            Employee fireEmployee = null;
        
            foreach (Employee employee in HiredEmployees)
            {
                if (employee.Data == employeeData)
                {
                    fireEmployee = employee;
                    break;
                }
            }

            if (HiredEmployees.Contains(fireEmployee))
            {
                HiredEmployees.Remove(fireEmployee);
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
    
    /// <summary>
    /// 고양이를 생성
    /// </summary>
    public void SpawnCat()
    {
        Instantiate(catPrefab, SpawnPoint.position, Quaternion.identity);
    }
    
    /// <summary>
    /// 고양이를 생성
    /// </summary>
    public void SpawnDog()
    {
        Instantiate(dogPrefab, SpawnPoint.position, Quaternion.identity);
    }
    
    
}
