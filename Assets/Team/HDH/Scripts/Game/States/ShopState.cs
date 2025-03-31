using UnityEngine;

public class ShopState : GameBaseState
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
            StateMachine.ChangeState(GameStateMachine.inGameState);
        }
    }
}
