using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private IMission[] missions;

    void Start()
    {
        Debug.Log(missions[0].GetScore());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
