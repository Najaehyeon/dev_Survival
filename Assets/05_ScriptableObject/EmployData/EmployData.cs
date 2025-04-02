using UnityEngine;


[CreateAssetMenu(fileName = "Employ Data", menuName = "NPC/Employ Data", order = int.MaxValue)]
public class EmployData : ScriptableObject
{
    [SerializeField] private int employIndex;
    public int EmployIndex { get { return employIndex; } }

    [SerializeField] private string employName;
    public string EmployName { get { return employName; } }

    [SerializeField] private Sprite employIcon;
    public Sprite EmployIcon { get { return employIcon; } }

    [SerializeField] private int price;
    public int Price { get { return price; } }

    [SerializeField] private int ability;
    public int Ability { get { return ability; } }

    [SerializeField] private int sincerity;
    public int Sincerity { get { return sincerity; } }

    [SerializeField] private float stressControl;
    public float StressControl { get { return stressControl; } }

    [SerializeField] private GameObject employeePrefab;
    public GameObject EmployeePrefab { get { return employeePrefab; } }
}


