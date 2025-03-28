using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : IStateMachine
{
    public InGameState inGameState;
    public ScoreState scoreState;
    public ShopState shopState;

    public BaseState CurrentState { get; set; }

    public void Init()
    {
        inGameState = new InGameState(this);
        scoreState = new ScoreState(this);
        shopState = new ShopState(this);

        ChangeState(inGameState);
    }

    public void ChangeState(GameBaseState state)
    {
        if(CurrentState != null)
            CurrentState.Exit();

        CurrentState = state;

        CurrentState.Enter();

    }

    public void Update()
    {
        CurrentState.Update();
    }

    public void ChangeState(BaseState state)
    {
        throw new NotImplementedException();
    }
}
