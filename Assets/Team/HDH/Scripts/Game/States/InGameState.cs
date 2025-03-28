using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class InGameState : GameBaseState
{
    float startTime = 540f;
    float unitSecond = 9 / 2f;
    float endDayTime = 10f;

    public InGameState(GameStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        SetTimerText();
    }

    void SetTimerText()
    {

        if (GameManager.Instance.PassedTime >= endDayTime)
        {
            //StateMachine.ChangeState(StateMachine.scoreState);
            return;
        }

        GameManager.Instance.PassTime(Time.deltaTime);
        GameManager.Instance.uiTest.TimerText.text = (startTime + GameManager.Instance.PassedTime * unitSecond).FormatTime();
    }
}
