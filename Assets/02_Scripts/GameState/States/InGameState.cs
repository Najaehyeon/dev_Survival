using System;
using UnityEngine;

[Serializable]
public class InGameState : GameBaseState
{
    float startTime = 540f;
    float unitSecond = 9 / 2f;
    float endDayTime = 120f;

    public InGameState(GameStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        MissionManager.Instance.ChangeState(MissionState.Ready);
        NPCManager.Instance.ActiveEmployees();

        if (NPCManager.Instance.hiredEmployees.Count > 0)
            NPCManager.Instance.ActiveEmployees();
    }

    public override void Exit()
    {
        MissionManager.Instance.IsDayEnd();
        if (NPCManager.Instance.hiredEmployees.Count > 0)
            NPCManager.Instance.InactiveEmployees();
    }

    public override void Update()
    {
        SetTimerText();
    }

    void SetTimerText()
    {

        if (GameManager.Instance.PassedTime >= endDayTime && ! GameManager.Instance.isMissionInProgress)
        {
            StateMachine.ChangeState(GameStateMachine.scoreState);
            UIManager.Instance.ChangeState(UIState.Score);
            return;
        }

        GameManager.Instance.PassTime(Time.deltaTime);
        UIManager.Instance.ChangeStatusUI(Status.Time, (startTime + GameManager.Instance.PassedTime * unitSecond));
        // GameManager.Instance.uiTest.TimerText.text = (startTime + GameManager.Instance.PassedTime * unitSecond).FormatTime();
    }
}
