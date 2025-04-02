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

    void Update()
    {
        stateMachine.StateUpdate();
        agent.SetDestination(stateMachine.CurrentNPCState.TargetDestination);
        if(agent.velocity.magnitude > 0.1f)
            animationHandler.IsMove(agent.velocity);
        else
            animationHandler.IsIdle();
    }
    
    /// <summary>
    /// NPC의 이동 속도를 변경
    /// </summary>
    /// <param name="moveSpeed">변경할 이동 속도 값</param>
    public void ChangeMoveSpeed(float moveSpeed)
    {
        agent.speed = moveSpeed;
    }
    
}
