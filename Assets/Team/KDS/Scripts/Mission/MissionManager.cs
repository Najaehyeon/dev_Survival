using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : Singleton<MissionManager>
{
    public List<MissionTimer> SelectedMissions;

    [Header("MissionControll")]
    public MissionController controller;

    public void IsDayEnd()
    {
        controller.IsDayEnd();
    }
    public void ChangeState(MissionState state)
    {
        controller.ChangeState(state);
    }
}
