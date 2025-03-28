using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateMachine
{
    BaseState CurrentState { get; set; }

    void ChangeState(BaseState newState);
}
