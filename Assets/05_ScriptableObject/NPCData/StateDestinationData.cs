using System;
using UnityEngine;
[CreateAssetMenu(fileName ="New DestinationSet", menuName = "NPC/DestinationSet")]
[Serializable]
public class StateDestinationData : ScriptableObject
{
    [field: SerializeField] public Vector3[] DestinationSet;
}
