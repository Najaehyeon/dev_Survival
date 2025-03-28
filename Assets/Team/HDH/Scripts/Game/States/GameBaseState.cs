using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameBaseState : BaseState
{
    protected GameBaseState(GameStateMachine stateMachine) : base(stateMachine)
    {
        StateMachine = stateMachine;
    }
}
