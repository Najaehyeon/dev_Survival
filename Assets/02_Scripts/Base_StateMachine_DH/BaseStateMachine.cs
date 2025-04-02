using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStateMachine : MonoBehaviour
{

    [field : SerializeField] public BaseState CurrentState { get; set; }

    public abstract void Init();

    public void StateUpdate()
    {
        if(CurrentState != null)
            CurrentState.Update();
    }

    public void ChangeState(BaseState state)
    {
        if (CurrentState != null)
            CurrentState.Exit();

        CurrentState = state;

        CurrentState.Enter();
    }
}
