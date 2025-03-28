using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCBaseState : BaseState
{
    protected NPCBaseState(NPCStateMachine stateMachine) : base(stateMachine)
    {
        StateMachine = stateMachine;
    }
}
