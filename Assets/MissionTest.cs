using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTest : MonoBehaviour
{
    MissionTimer missionTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        missionTimer = GetComponent<MissionTimer>();
        StartCoroutine(StartMission());
    }

    IEnumerator StartMission()
    {
        yield return new WaitForSeconds(11f);
        missionTimer.Selected();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
