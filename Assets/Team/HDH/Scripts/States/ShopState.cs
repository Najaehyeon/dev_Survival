using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopState : BaseState
{
    public ShopState(GameStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        GameManager.Instance.SetTimeZero();
        GameManager.Instance.PassDay();
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(stateMachine.inGameState);
        }
    }
}
