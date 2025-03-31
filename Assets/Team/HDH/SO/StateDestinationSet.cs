using System;
using UnityEngine;
[CreateAssetMenu(fileName ="New DestinationSet", menuName = "NPC/DestinationSet")]
[Serializable]
public class StateDestinationSet : ScriptableObject
{
    [field: SerializeField] public string StateName;
    [field: SerializeField] public Vector3[] DestinationSet;
}
