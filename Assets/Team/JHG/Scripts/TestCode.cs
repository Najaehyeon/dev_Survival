using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCode : MonoBehaviour
{
    public GameObject missionPrefab;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayCodeMission();
        }
    }

    public void PlayCodeMission()
    {
        Instantiate(missionPrefab, Vector3.zero, Quaternion.identity);
    }
}
