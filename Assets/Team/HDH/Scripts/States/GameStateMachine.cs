using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine
{
    BaseState currentState;

    public InGameState inGameState;
    public ScoreState scoreState;
    public ShopState shopState;


    public void Init()
    {
        inGameState = new InGameState(this);
        scoreState = new ScoreState(this);
        shopState = new ShopState(this);

        ChangeState(inGameState);
    }

    public void ChangeState(BaseState state)
    {
        if(currentState != null)
            currentState.Exit();

        currentState = state;

        Debug.Log(currentState.ToString());

        currentState.Enter();

    }

    public void Update()
    {
        currentState.Update();
    }
}
