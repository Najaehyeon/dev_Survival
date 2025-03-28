using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    IStateMachine StateMachine { get; set; }

    void Enter();
    void Exit();
    void Update();
}
