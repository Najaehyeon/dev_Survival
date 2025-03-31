using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    NavMeshAgent agent;
    NPCStateMachine stateMachine;
    
    private int destinationIndex;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        stateMachine = GetComponent<NPCStateMachine>();
        stateMachine.Init();
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.StateUpdate();
        agent.SetDestination(stateMachine.CurrentNPCState.TargetDestination);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Mission에 해당하는 장소에 충돌시 OnMission() 함수 실행

        MissionTimer missionTimer;
        if (other.TryGetComponent<MissionTimer>(out missionTimer))
        {
            stateMachine.CurrentNPCState.OnMission(missionTimer);
        }
    }
}
