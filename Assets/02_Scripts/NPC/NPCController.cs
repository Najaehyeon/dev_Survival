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
    AnimationHandler animationHandler;
    
    private int destinationIndex;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        
        stateMachine = GetComponent<NPCStateMachine>();
        stateMachine.Init();

        animationHandler = GetComponent<AnimationHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.StateUpdate();
        agent.SetDestination(stateMachine.CurrentNPCState.TargetDestination);
        if(agent.velocity.magnitude > 0.1f)
            animationHandler.IsMove(agent.velocity);
        else
            animationHandler.IsIdle();
    }

    public void ChangeMoveSpeed(float moveSpeed)
    {
        agent.speed = moveSpeed;
    }
    
}
