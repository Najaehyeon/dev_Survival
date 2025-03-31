using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMission : Mission
{
    [SerializeField] MissionTimer[] catMissionTimers;

    private void Start()
    {
        foreach (MissionTimer timer in catMissionTimers)
        {
            timer.mission = this;
        }
    }
    
    
}
