using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EmployeeManager : Singleton<EmployeeManager>
{
    private Dictionary<int, GameObject> hiredEmployees = new Dictionary<int, GameObject>();
    private Dictionary<int, bool> employeeSpawned = new Dictionary<int, bool>();
    
    //NPC 업무 할당을 위한 Queue
    public Queue<NPCStateMachine> IdleEmployees = new Queue<NPCStateMachine>();
    
    public Transform StartPos;
    
    //같은 스프라이트에도 여러 스탯이 가능
    //스탯이 SO에 있으니
    //상점에서도 인덱스를 가지고 있고 NPC 매니저에
    
    //데이터에는 스탯 정보랑 인덱스, 이름
    //스프라이트랑 생성할 때 게임오브젝트가 필요한데
    //데이터에 프리팹까지 할당
    
    //상점에서 인덱스를 받기만 하면 됨

    //직원 생성, 직원 업무 할당

    public void HireEmployee(int index)
    {
        
    }
    
}
