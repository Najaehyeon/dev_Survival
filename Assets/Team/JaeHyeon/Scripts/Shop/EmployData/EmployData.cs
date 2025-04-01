using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Employ Data", menuName = "NPC/Employ Data", order = int.MaxValue)]
public class EmployData : ScriptableObject
{
    [SerializeField] private int employIndex;
    public int EmployIndex { get { return employIndex; } }

    [SerializeField] private string employName;
    public string EmployName { get { return EmployName; } }

    [SerializeField] private int price;
    public int Price { get { return price; } }

    [SerializeField] private int efficiency;
    public int Efficiency { get { return Efficiency; } }

    [SerializeField] private int ability;
    public int Ability { get { return Ability; } }

    [SerializeField] private int sincerity;
    public int Sincerity { get { return Sincerity; } }

    [SerializeField] private int stressControl;
    public int StressControl { get { return StressControl; } }


}


