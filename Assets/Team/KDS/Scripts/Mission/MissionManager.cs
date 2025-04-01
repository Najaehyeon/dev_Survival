using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : Singleton<MissionManager>
{
    public List<MissionTimer> SelectedMissions;

    [Header("MissionControll")]
    public MissionController controller;

    /// <summary>
    /// 게임매니저에서 호출용
    /// State전환시 호출
    /// </summary>
    public void IsDayEnd()
    {
        controller.IsDayEnd();
    }

    /// <summary>
    /// 외부에서 State전환시 호출
    /// </summary>
    /// <param name="state"></param>
    public void ChangeState(MissionState state)
    {
        controller.ChangeState(state);
    }
}
