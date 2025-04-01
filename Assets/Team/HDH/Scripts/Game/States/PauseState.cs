using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : GameBaseState
{
    public PauseState(GameStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        Debug.Log("퍼즈스테이트 종료");
        //throw new System.NotImplementedException();
    }

    public override void Update()
    {
        //throw new System.NotImplementedException();
    }
}
