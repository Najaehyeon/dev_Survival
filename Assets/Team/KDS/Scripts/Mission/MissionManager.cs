using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : Singleton<MissionManager>
{
    [SerializeField] public Mission[] missions;


    [Header("MissionControll")]
    public MissionController controller;
    void Start()
    {        
    }
    void Update()
    {
        
    }
}
