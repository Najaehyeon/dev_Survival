using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    NavMeshAgent agent;
    NPCStateMachine stateMachine;

    [SerializeField] public StateDestinationSet IdleDestinationSet;
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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(stateMachine.npcMissionState);
        }
    }

}
