using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : MonoBehaviour
{
    public EmployeeStates EmployeeStates { get; private set; }

    private void Start()
    {
        EmployeeStates = GetComponent<EmployeeStates>();
    }
}
